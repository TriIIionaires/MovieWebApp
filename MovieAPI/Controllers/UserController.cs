using Microsoft.AspNetCore.Mvc;
using MovieDLL.Data;
using MovieDLL.Models;

namespace MovieAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class UserController : ControllerBase
	{
		IUserData _db = new UserData();

		[HttpGet("search/{username}")]
		public async Task<UserModel> ReadUser(string username)
		{
			UserModel result = await _db.ReadUser(username);
			return result;
		}

		[HttpGet("available/{username}")]
		public async Task<bool> CheckUsernameAvailable(string username)
		{
			bool isAvailable = await _db.CheckUsernameAvailable(username);
			return isAvailable;
		}

		[HttpPost("create/")]
		public void CreateUser(UserModel user)
		{
			_db.CreateUser(user);
		}

	}
}
