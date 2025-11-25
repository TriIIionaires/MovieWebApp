using MovieDLL.Models;

namespace MovieUI.Services
{
	public interface IUserService
	{
		Task<UserModel> GetUser(string username);
		Task<bool> CreateUser(string username, string pwd);
		Task<bool> ValidateUser(string username, string pwd);
	}
}