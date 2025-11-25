using MovieDLL.Models;

namespace MovieUI.Services
{
	public interface IRatingService
	{
		void CreateRating(int user_id, int movie_id, double rating);
		Task<RatingModel> GetRating(int user_id, int movie_id);
		Task<(double RootMeanSquaredError, double RSquared)> GetModelEstimate();
		Task<List<MovieModel>> GetUserRecommendations(int user_id, int limit);
		void UpdateRating(int user_id, int movie_id, double rating);
	}
}