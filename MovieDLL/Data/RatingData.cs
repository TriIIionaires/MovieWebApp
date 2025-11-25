using MovieDLL.Models;

namespace MovieDLL.Data
{
	public class RatingData : IRatingData
	{
		private readonly ISqlDataAccess _db = new SqlDataAccess();

		public void CreateUserRating(RatingModel rating)
		{
			string sql = "INSERT INTO user_rating (User_ID, Movie_ID, Rating) VALUES (@User_ID, @Movie_ID, @Rating)";

			try
			{
				_db.SaveData(sql, rating);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}

		public async Task<RatingModel> ReadRating(int user_id, int movie_id)
		{
			string sql = "SELECT * FROM user_rating WHERE user_rating.User_ID = @user_id AND user_rating.Movie_ID = @movie_id";

			List<RatingModel> result = _db.LoadData<RatingModel, dynamic>(sql, new { user_id, movie_id });

			if (result.Count > 0) return result[0];
			return null;
		}

		public void UpdateUserRating(RatingModel rating)
		{
			string sql = "UPDATE user_rating SET User_ID = @User_ID, Movie_ID = @Movie_ID, Rating = @Rating WHERE user_rating.Movie_ID = @Movie_ID";

			try
			{
				_db.SaveData(sql, rating);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}
		}


	}
}
