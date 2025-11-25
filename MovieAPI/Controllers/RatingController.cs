using Microsoft.AspNetCore.Mvc;
using MovieDLL.Data;
using MovieDLL.Models;

namespace MovieAPI.Controllers
{
	[Route("api/[controller]")]
	[ApiController]
	public class RatingController : ControllerBase
	{
		IRatingData _db = new RatingData();

		[HttpGet("userid={user_id}&movieid={movie_id}")]
		public async Task<RatingModel> ReadRating(int user_id, int movie_id)
		{
			RatingModel result = await _db.ReadRating(user_id, movie_id);
			return result;
		}

		[HttpPost("create/")]
		public void CreateRating(RatingModel rating)
		{
			_db.CreateUserRating(rating);
		}

		[HttpPut("update/")]
		public void UpdateRating(RatingModel rating)
		{
			_db.UpdateUserRating(rating);
		}

	}
}
