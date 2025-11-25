using MovieDLL.Models;

namespace MovieUI.Services
{
	public class RatingService : IRatingService
	{
		private readonly HttpClient _httpClient;

		public RatingService(HttpClient httpClient)
		{
			_httpClient = httpClient;
		}

		public async Task<RatingModel> GetRating(int user_id, int movie_id)
		{
			try
			{
				RatingModel rating = await _httpClient.GetFromJsonAsync<RatingModel>($"/api/Rating/userid={user_id}&movieid={movie_id}");

				return rating;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<(double RootMeanSquaredError, double RSquared)> GetModelEstimate()
		{
			try
			{
				return await _httpClient.GetFromJsonAsync<(double RootMeanSquaredError, double RSquared)>($"/api/Recommendation/model/estimate");
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
				return (0, 0);
            }
		}

		public async Task<List<MovieModel>> GetUserRecommendations(int user_id, int limit)
		{
			try
			{
				List<MovieModel> movies = await _httpClient.GetFromJsonAsync<List<MovieModel>>($"/api/Recommendation/userid={user_id}&limit={limit}");

				return movies;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		public async void CreateRating(int user_id, int movie_id, double rating)
		{
			try
			{
				RatingModel userRating = new RatingModel()
				{
					User_ID = user_id,
					Movie_ID = movie_id,
					Rating = rating
				};

				await _httpClient.PostAsJsonAsync<RatingModel>($"/api/Rating/create", userRating);
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
            }
		}

		public async void UpdateRating(int user_id, int movie_id, double rating)
		{
			try
			{
				RatingModel userRating = new RatingModel()
				{
					User_ID = user_id,
					Movie_ID = movie_id,
					Rating = rating
				};

				await _httpClient.PutAsJsonAsync<RatingModel>($"/api/Rating/update", userRating);
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
            }
		}

	}
}
