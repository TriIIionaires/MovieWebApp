using Microsoft.AspNetCore.Mvc;
using Microsoft.ML;
using Microsoft.ML.Data;
using Microsoft.ML.Trainers;
using MovieDLL.Data;
using MovieDLL.Models;
using MySqlConnector;

namespace MovieAPI.Controllers
{
	public class MovieRating
	{
		[LoadColumn(0)]
		public float User_ID;
		[LoadColumn(1)]
		public float Movie_ID;
		[LoadColumn(2)]
		public float Rating;
	}

	public class MovieRatingPrediction
	{
		public float Label;
		public float Score;
	}

	[Route("api/[controller]")]
	[ApiController]
	public class RecommendationController : ControllerBase
	{

		MLContext mlContext = new MLContext();
		IMovieData _db = new MovieData();
		static ITransformer? model;

		private IDataView LoadTrainingData(MLContext mlContext)
		{
			string connectionString = "server=localhost; port=3306; uid=TheaterUser; pwd=AppDevRules!; database=theater_metadata";

			string sql = "SELECT CAST(User_ID AS DECIMAL), CAST(Movie_ID AS DECIMAL), CAST(Rating AS DECIMAL) FROM user_rating";

			DatabaseLoader loader = mlContext.Data.CreateDatabaseLoader<MovieRating>();
			DatabaseSource dbSource = new DatabaseSource(MySqlConnectorFactory.Instance, connectionString, sql);

			IDataView trainingDataView = loader.Load(dbSource);
			return trainingDataView;
		}

		private IDataView LoadTestData(MLContext mlContext)
		{
			var testDataPath = Path.Combine(Environment.CurrentDirectory, "Data", "recommendation-ratings-test.csv");

			IDataView testDataView = mlContext.Data.LoadFromTextFile<MovieRating>(testDataPath, hasHeader: true, separatorChar: ',');

			return testDataView;
		}

		private ITransformer BuildAndTrainModel(MLContext mlContext, IDataView trainingDataView)
		{
			IEstimator<ITransformer> estimator = mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "User_IDEncoded", inputColumnName: "User_ID")
			.Append(mlContext.Transforms.Conversion.MapValueToKey(outputColumnName: "Movie_IDEncoded", inputColumnName: "Movie_ID"));
			
			var options = new MatrixFactorizationTrainer.Options
			{
				MatrixColumnIndexColumnName = "User_IDEncoded",
				MatrixRowIndexColumnName = "Movie_IDEncoded",
				LabelColumnName = "Rating",
				NumberOfIterations = 20,
				ApproximationRank = 100
			};

			var trainerEstimator = estimator.Append(mlContext.Recommendation().Trainers.MatrixFactorization(options));

			Console.WriteLine("=============== Training the model ===============");
			ITransformer model = trainerEstimator.Fit(trainingDataView);

			return model;
		}

		private (double RootMeanSquaredError, double RSquared) EvaluateModel(MLContext mlContext, IDataView testDataView, ITransformer model)
		{
			Console.WriteLine("=============== Evaluating the model ===============");
			var prediction = model.Transform(testDataView);

			var metrics = mlContext.Regression.Evaluate(prediction, labelColumnName: "Rating", scoreColumnName: "Score");

			return (metrics.RootMeanSquaredError , metrics.RSquared);
		}

		private async Task<MovieModel> UseModelForSinglePrediction(MLContext mlContext, ITransformer model, int user_id, int movie_id)
		{
			Console.WriteLine("=============== Making a prediction ===============");
			var predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
						
			var testInput = new MovieRating { User_ID = user_id, Movie_ID = movie_id };
			var movieRatingPrediction = predictionEngine.Predict(testInput);

			if (Math.Round(movieRatingPrediction.Score, 1) > 1)
			{
				
				MovieModel movie = await _db.ReadByMovieID(movie_id);

				if (movie != null)
				{
					List<GenreModel> genres = _db.ReadMovieGenres(movie_id);
					movie.Genres = genres;
				}

				return movie;
				
			}
			return null;

		}

		private async Task<List<MovieModel>> UseModelForMultiplePredictions(MLContext mlContext, ITransformer model, int user_id, int limit)
		{
			Console.WriteLine("=============== Making a prediction ===============");
			var predictionEngine = mlContext.Model.CreatePredictionEngine<MovieRating, MovieRatingPrediction>(model);
			
			Random random = new Random();
			List<MovieModel> recommendedMovies = new List<MovieModel>();

			while (recommendedMovies.Count < limit)
			{
				int movie_id = random.Next(1, 32262);

				MovieRating testInput = new MovieRating { User_ID = user_id, Movie_ID = movie_id };
				MovieRatingPrediction movieRatingPrediction = predictionEngine.Predict(testInput);

				if (Math.Round(movieRatingPrediction.Score, 1) > 1)
				{

					MovieModel movie = await _db.ReadByMovieID(movie_id);

					if (movie != null)
					{
						bool isRepeated = false;

						foreach (MovieModel m in recommendedMovies)
						{
							if (m.Movie_ID == movie.Movie_ID) isRepeated = true;
						}

						if (isRepeated == false)
						{
							List<GenreModel> genres = _db.ReadMovieGenres(movie_id);
							movie.Genres = genres;
							recommendedMovies.Add(movie);
						}
						
					}

				}

			}

			return recommendedMovies;

		}

		[HttpGet("model/estimate")]
		public (double RootMeanSquaredError, double RSquared) EstimateModel()
		{
			if (model != null)
			{
				IDataView testDataView = LoadTestData(mlContext);
				return EvaluateModel(mlContext, testDataView, model);
			}
			return (0, 0);
		}

		[HttpGet("userid={user_id}&limit={limit}")]
		public async Task<List<MovieModel>> GetRecommendations(int user_id, int limit)
		{
			IDataView trainingDataView = LoadTrainingData(mlContext);
			model = BuildAndTrainModel(mlContext, trainingDataView);
			
			if (model != null) return await UseModelForMultiplePredictions(mlContext, model, user_id, limit);

			return null;
			
		}

	}
}
