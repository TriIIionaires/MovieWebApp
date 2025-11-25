using MovieDLL.Models;

namespace MovieDLL.Data
{
	public interface IRatingData
	{
		void CreateUserRating(RatingModel rating);
		Task<RatingModel> ReadRating(int user_id, int movie_id);
		void UpdateUserRating(RatingModel rating);
	}
}