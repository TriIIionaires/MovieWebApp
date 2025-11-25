using MovieDLL.Models;

namespace MovieDLL.Data
{
	public interface IUserData
	{
		Task<bool> CheckUsernameAvailable(string username);
		void CreateUser(UserModel user);
		void DeleteUser(int id);
		Task<UserModel> ReadUser(string username);
		void UpdateUser(UserModel user);
	}
}