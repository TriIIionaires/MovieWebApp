using MovieDLL.Models;

namespace MovieUI.Services
{
    public interface IMovieService
    {
        Task<MovieModel> GetMovieByID(int id);
		Task<MovieModel> GetMovieByMovieID(int movie_id);
		Task<List<MovieModel>> GetMoviesByTitle(string title, int votes, int limit);
		Task<List<MovieModel>> GetMoviesByRating(int min, int max, int votes, int limit);
	}
}