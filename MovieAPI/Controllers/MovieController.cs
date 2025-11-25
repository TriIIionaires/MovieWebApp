using Microsoft.AspNetCore.Mvc;
using MovieDLL.Data;
using MovieDLL.Models;

namespace MovieAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class MovieController : ControllerBase
    {
        IMovieData _db = new MovieData();

		[HttpGet("id={id}")]
		public async Task<MovieModel> ReadByID(int id)
		{
			MovieModel result = await _db.ReadByID(id);

			List<GenreModel> genres = _db.ReadMovieGenres(result.Movie_ID);
			result.Genres = genres;

			return result;
		}

		[HttpGet("movieid={movie_id}")]
        public async Task<MovieModel> ReadByMovieID(int movie_id)
        {
            MovieModel result = await _db.ReadByMovieID(movie_id);

            List<GenreModel> genres = _db.ReadMovieGenres(result.Movie_ID);
            result.Genres = genres;

            return result;
        }

        [HttpGet("title={title}&votes={votes}&limit={limit}")]
        public async Task<List<MovieModel>> ReadByTitle(string title, int votes, int limit)
        {
            List<MovieModel> result = await _db.ReadByTitle(title, votes, limit);

            foreach (MovieModel movie in result)
            {
                List<GenreModel> genres = _db.ReadMovieGenres(movie.Movie_ID);
                movie.Genres = genres;
            }

            return result;
        }

        [HttpGet("description={desc}&votes={votes}&limit={limit}")]
        public async Task<List<MovieModel>> ReadByDescription(string desc, int votes, int limit)
        {
            List<MovieModel> result = await _db.ReadByDescription(desc, votes, limit);

            foreach (MovieModel movie in result)
            {
                List<GenreModel> genres = _db.ReadMovieGenres(movie.Movie_ID);
                movie.Genres = genres;
            }

            return result;
        }

        [HttpGet("min={shortest}&max={longest}&votes={votes}&limit={limit}")]
        public async Task<List<MovieModel>> ReadByRuntime(int shortest, int longest, int votes, int limit)
        {
            List<MovieModel> result = await _db.ReadByRuntime(shortest, longest, votes, limit);

            foreach (MovieModel movie in result)
            {
                List<GenreModel> genres = _db.ReadMovieGenres(movie.Movie_ID);
                movie.Genres = genres;
            }

            return result;
        }

        [HttpGet("startDate={start}&endDate={end}&limit={limit}")]
        public async Task<List<MovieModel>> ReadByDate(DateTime start, DateTime end, int limit)
        {
            List<MovieModel> result = await _db.ReadByDate(start, end, limit);

            foreach (MovieModel movie in result)
            {
                List<GenreModel> genres = _db.ReadMovieGenres(movie.Movie_ID);
                movie.Genres = genres;
            }

            return result;
        }

        [HttpGet("genre_id={genre_id}&votes={votes}&limit={limit}")]
        public async Task<List<MovieModel>> ReadByGenreID(int genre_id, int votes, int limit)
        {
            List<MovieModel> result = await _db.ReadByGenreID(genre_id, votes, limit);

            foreach (MovieModel movie in result)
            {
                List<GenreModel> genres = _db.ReadMovieGenres(movie.Movie_ID);
                movie.Genres = genres;
            }

            return result;
        }

        [HttpGet("genres")]
        public async Task<List<GenreModel>> ReadAllGenres()
        {
            List<GenreModel> result = await _db.ReadAllGenres();
            return result;
        }

    }
}
