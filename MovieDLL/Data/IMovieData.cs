using MovieDLL.Models;

namespace MovieDLL.Data
{
    public interface IMovieData
    {
        void CreateAssignGenre(List<GenreModel> genres);
        void CreateMovie(MovieModel movie);
        void DeleteMovie(int id);
        Task<List<GenreModel>> ReadAllGenres();
        Task<List<MovieModel>> ReadAllMovies();
        Task<List<MovieModel>> ReadByRating(int min, int max, int votes, int limit);
		Task<List<MovieModel>> ReadByDate(DateTime start, DateTime end, int limit);
        Task<List<MovieModel>> ReadByDescription(string desc, int votes, int limit);
        Task<List<MovieModel>> ReadByGenreID(int genre_id, int votes, int limit);
        Task<MovieModel> ReadByID(int id);
        Task<MovieModel> ReadByMovieID(int movie_id);

		Task<List<MovieModel>> ReadByRuntime(int shortest, int longest, int votes, int limit);
        Task<List<MovieModel>> ReadByTitle(string title, int votes, int limit);
        List<GenreModel> ReadMovieGenres(int movie_id);
        void UpdateMovie(MovieModel movie);
    }
}