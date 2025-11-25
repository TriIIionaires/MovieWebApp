using MovieDLL.Models;

namespace MovieDLL.Data
{
	public class UserData : IUserData
	{
		private readonly ISqlDataAccess _db = new SqlDataAccess();

		public async Task<bool> CheckUsernameAvailable(string username)
		{
			string sql = "SELECT ID FROM user WHERE user.username = @username";

			List<int> result = _db.LoadData<int, dynamic>(sql, new { username });

			if (result.Count > 0) return false;
			return true;
		}

		public async Task<UserModel> ReadUser(string username)
		{
			string sql = "SELECT * FROM user WHERE user.username = @username";

			List<UserModel> result = _db.LoadData<UserModel, dynamic>(sql, new { username });

			if (result.Count > 0) return result[0];
			return null;
		}

		public void CreateUser(UserModel user)
		{
			string sql = "INSERT INTO user (Username, PwdHash, Salt) VALUES (@Username, @PwdHash, @Salt)";

			try
			{
				_db.SaveData(sql, user);
			} 
			catch (Exception ex)
			{
                Console.WriteLine(ex.Message);
            }
			
		}

		public void UpdateUser(UserModel user)
		{
			string sql = "UPDATE user SET Username = @Username, PwdHash = @PwdHash, Salt = @Salt WHERE user.ID = @ID";

			try
			{
				_db.SaveData(sql, user);
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
			}

		}

		public void DeleteUser(int id)
		{
			string sql = "DELETE FROM user WHERE user.ID = @id";

			try
			{
				_db.SaveData(sql, new { id });
			}
			catch (Exception ex)
			{
                Console.WriteLine(ex);
            }
			
		}

	}
}
