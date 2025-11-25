using MovieDLL.Models;

namespace MovieDLL.Data
{
    public class MovieData : IMovieData
    {
        private readonly ISqlDataAccess _db = new SqlDataAccess();

        public async Task<List<MovieModel>> ReadAllMovies()
        {
            string sql = "SELECT mmd_movie.ID, Movie_ID, IMDB_ID, Title, Release_Date, RunTime, mmd_ratings.mpaa_rating, Rating, Votes, Tagline, Description, Homepage, PosterURL " +
              "FROM mmd_movie " +
              "INNER JOIN mmd_ratings ON mmd_movie.MPAA_RatingID = mmd_ratings.mpaa_ratingid";

            IEnumerable<MovieModel> result = _db.LoadData<MovieModel, dynamic>(sql, new { });
            List<MovieModel> movies = result.ToList();

            if (movies.Count > 0) return movies;
            return null;
        }

        public async Task<MovieModel> ReadByID(int id)
        {
            string sql = "SELECT mmd_movie.ID, mmd_movie.Movie_ID, IMDB_ID, Title, Release_Date, RunTime, mmd_ratings.mpaa_rating, Rating, Votes, Tagline, Description, Homepage, PosterURL " +
              "FROM mmd_movie " +
              "INNER JOIN mmd_ratings ON mmd_movie.MPAA_RatingID = mmd_ratings.mpaa_ratingid " +
              "WHERE mmd_movie.ID = @id";

            List<MovieModel> movies = _db.LoadData<MovieModel, dynamic>(sql, new { id });

            if (movies.Count > 0) return movies[0];
            return null;
        }

		public async Task<MovieModel> ReadByMovieID(int movie_id)
		{
			string sql = "SELECT mmd_movie.ID, mmd_movie.Movie_ID, IMDB_ID, Title, Release_Date, RunTime, mmd_ratings.mpaa_rating, Rating, Votes, Tagline, Description, Homepage, PosterURL " +
			  "FROM mmd_movie " +
			  "INNER JOIN mmd_ratings ON mmd_movie.MPAA_RatingID = mmd_ratings.mpaa_ratingid " +
			  "WHERE mmd_movie.Movie_ID = @movie_id";

			List<MovieModel> movies = _db.LoadData<MovieModel, dynamic>(sql, new { movie_id });

			if (movies.Count > 0) return movies[0];
			return null;
		}

		// Sample Code
		public async Task<List<MovieModel>> ReadByGenreID(int genre_id, int votes, int limit)
        {
            string sql = "SELECT mmd_movie.ID, mmd_movie.Movie_ID, IMDB_ID, Title, Release_Date, RunTime, mmd_ratings.mpaa_rating, Rating, Votes, Tagline, Description, Homepage, PosterURL " +
              "FROM mmd_movie " +
              "INNER JOIN mmd_ratings ON mmd_movie.MPAA_RatingID = mmd_ratings.mpaa_ratingid " +
              "INNER JOIN assign_genre ON mmd_movie.Movie_ID = assign_genre.movie_id " +
              "WHERE assign_genre.genre_id = @genre_id " +
              "AND votes > @votes " +
              "ORDER BY Rating DESC " +
              "LIMIT @limit";

            List<MovieModel> result = _db.LoadData<MovieModel, dynamic>(sql, new { genre_id, votes, limit });

            if (result.Count > 0) return result;
            return null;
        }

        // Student Code (Jadon, Omar, Gavin, Kevin, Nathan, Angel, Daniel)
        public async Task<List<MovieModel>> ReadByDate(DateTime start, DateTime end, int limit)
        {
            string sql = "SELECT mmd_movie.ID, Movie_ID, IMDB_ID, Title, Release_Date, RunTime, mmd_ratings.mpaa_rating, Rating, Votes, Tagline, Description, Homepage, PosterURL " +
              "FROM mmd_movie " +
              "INNER JOIN mmd_ratings ON mmd_movie.MPAA_RatingID = mmd_ratings.mpaa_ratingid " +
              "WHERE mmd_movie.Release_Date BETWEEN DATE(@start) AND DATE(@end) " +
              "ORDER BY Release_Date ASC " +
              "LIMIT @limit";

            List<MovieModel> result = _db.LoadData<MovieModel, dynamic>(sql, new { start, end, limit });

            if (result.Count > 0) return result;
            return null;
        }

        // Student Code (Mohamad, Anthony, Peter, Conrad)
        public async Task<List<MovieModel>> ReadByRuntime(int shortest, int longest, int votes, int limit)
        {
            string sql = "SELECT mmd_movie.ID, Movie_ID, IMDB_ID, Title, Release_Date, RunTime, mmd_ratings.mpaa_rating, Rating, Votes, Tagline, Description, Homepage, PosterURL " +
              "FROM mmd_movie " +
              "INNER JOIN mmd_ratings ON mmd_movie.MPAA_RatingID = mmd_ratings.mpaa_ratingid " +
              "WHERE mmd_movie.RunTime BETWEEN @shortest AND @longest " +
              "AND mmd_movie.Votes > @votes " +
              "ORDER BY Rating DESC " +
              "LIMIT @limit";

            List<MovieModel> result = _db.LoadData<MovieModel, dynamic>(sql, new { shortest, longest, votes, limit });

            if (result.Count > 0) return result;
            return null;
        }

        // Student Code (Isabella, Maribel, Dhanwi, Simone, Marina, Rya)
        public async Task<List<MovieModel>> ReadByTitle(string title, int votes, int limit)
        {
            string sql = "SELECT mmd_movie.ID, Movie_ID, IMDB_ID, Title, Release_Date, RunTime, mmd_ratings.mpaa_rating, Rating, Votes, Tagline, Description, Homepage, PosterURL " +
              "FROM mmd_movie " +
              "INNER JOIN mmd_ratings ON mmd_movie.MPAA_RatingID = mmd_ratings.mpaa_ratingid " +
              "WHERE mmd_movie.Title LIKE CONCAT('%', @title, '%') " +
              "AND mmd_movie.Votes >= @votes " +
              "ORDER BY Rating DESC " +
              "LIMIT @limit";

            List<MovieModel> result = _db.LoadData<MovieModel, dynamic>(sql, new { title, votes, limit });
            if (result.Count > 0) return result;
            return null;
        }

        public async Task<List<MovieModel>> ReadByDescription(string desc, int votes, int limit)
        {
            string sql = "SELECT mmd_movie.ID, Movie_ID, IMDB_ID, Title, Release_Date, RunTime, mmd_ratings.mpaa_rating, Rating, Votes, Tagline, Description, Homepage, PosterURL " +
             "FROM mmd_movie " +
             "INNER JOIN mmd_ratings ON mmd_movie.MPAA_RatingID = mmd_ratings.mpaa_ratingid " +
             "WHERE mmd_movie.Description LIKE CONCAT('%', @desc, '%') " +
             "AND mmd_movie.Votes > @votes " +
             "ORDER BY mmd_movie.Rating DESC " +
             "LIMIT @limit";

            List<MovieModel> result = _db.LoadData<MovieModel, dynamic>(sql, new { desc, votes, limit });
            if (result.Count > 0) return result;
            return null;
        }

        public void CreateMovie(MovieModel movie)
        {
            string sql = "INSERT INTO mmd_movie (Movie_ID, IMDB_ID, Title, Release_Date, RunTime, MPAA_RatingID, Rating, Votes, Tagline, Description, Homepage, PosterURL) VALUES (@Movie_ID, @IMDB_ID, @Title, @Release_Date, @RunTime, @MPAA_Rating, @Rating, @Votes, @Tagline, @Description, @Homepage, @PosterURL)";

            _db.SaveData(sql, movie);
        }

        public void UpdateMovie(MovieModel movie)
        {
            string sql = "UPDATE mmd_movie SET Movie_ID = @Movie_ID, IMDB_ID = @IMDB_ID, Title = @Title, RunTime = @RunTime, MPAA_RatingID = @MPAA_Rating, Rating = @Rating, Votes = @Votes, Tagline = @Tagline, Description = @Description, Homepage = @Homepage, PosterURL = @PosterURL WHERE mmd_movie.ID = @ID";

            _db.SaveData(sql, movie);
        }

        public void DeleteMovie(int id)
        {
            string sql = "DELETE FROM mmd_movie WHERE mmd_movie.ID = @id";

            _db.SaveData(sql, new { id });
        }

        public void CreateAssignGenre(List<GenreModel> genres)
        {
            string sql = "INSERT INTO assign_genre (movie_id, genre_id) VALUES (@Movie_ID, @Genre_ID)";

            _db.SaveData(sql, genres);
        }

        public async Task<List<GenreModel>> ReadAllGenres()
        {
            string sql = "SELECT * FROM mmd_genres";
            List<GenreModel> result = _db.LoadData<GenreModel, dynamic>(sql, new { });

            if (result.Count > 0) return result;
            return null;
        }

        public List<GenreModel> ReadMovieGenres(int movie_id)
        {
            string sql = "SELECT mmd_genres.genre, mmd_genres.genre_id " +
              "FROM mmd_genres " +
              "INNER JOIN assign_genre ON mmd_genres.genre_ID = assign_genre.genre_ID " +
              "WHERE assign_genre.movie_ID = @movie_id";

            List<GenreModel> result = _db.LoadData<GenreModel, dynamic>(sql, new { movie_id });

            if (result.Count > 0) return result;
            return null;
        }

    }
}
