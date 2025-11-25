using MovieDLL.Models;

namespace MovieUI.Services
{
    public class MovieService : IMovieService
    {
        private readonly HttpClient _httpClient;

        public MovieService(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<MovieModel> GetMovieByID(int id)
        {
            try
            {
				MovieModel movie = await _httpClient.GetFromJsonAsync<MovieModel>($"/api/Movie/id={id}");

				return movie;
			}
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                return null;
            }
        }

		public async Task<MovieModel> GetMovieByMovieID(int movie_id)
		{
			try
			{
				MovieModel movie = await _httpClient.GetFromJsonAsync<MovieModel>($"/api/Movie/movieid={movie_id}");

				return movie;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}

		public async Task<List<MovieModel>> GetMoviesByTitle(string title, int votes, int limit)
        {
			try
			{
				List<MovieModel> movies = await _httpClient.GetFromJsonAsync<List<MovieModel>>($"/api/Movie/title={title}&votes={votes}&limit={limit}");

				return movies;
			}
			catch (Exception ex)
			{
				Console.WriteLine(ex.Message);
				return null;
			}
		}
    }
}
