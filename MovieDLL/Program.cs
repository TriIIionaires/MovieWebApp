using MovieDLL.Data;
using MovieDLL.Models;

namespace MovieDLL
{
    internal class Program
    {
        static void Main(string[] args)
        {
            IMovieData _db = new MovieData();

            /*List<GenreModel> mmd_genres = _db.ReadAllGenres();
            
            foreach (GenreModel genre in mmd_genres)
            {
                Console.WriteLine(genre);
            }

            Console.WriteLine("\nEnter a genre ID to search using the list above:");
            int genre_id = Int32.Parse(Console.ReadLine());

            Console.WriteLine("Enter a minimum number of votes:");
            int voteCount = Int32.Parse(Console.ReadLine());*/

            Console.WriteLine("Enter a maximum number of records:");
            int max;
            bool isMax = Int32.TryParse(Console.ReadLine(), out max);

            while (!isMax)
            {
                Console.WriteLine("Enter a valid number:");
                isMax = Int32.TryParse(Console.ReadLine(), out max);
            }

            Console.WriteLine("Enter a starting date (YYYY-MM-DD):");
            DateTime start;
            bool isStartDate = DateTime.TryParse(Console.ReadLine(), out start);

            while (!isStartDate)
            {
                Console.WriteLine("Enter a valid date (YYYY-MM-DD):");
                isStartDate = DateTime.TryParse(Console.ReadLine(), out start);
            }

            Console.WriteLine("Enter an ending date (leave blank if not applicable):");
            string response = Console.ReadLine();

            DateTime end;
            bool isEndDate = DateTime.TryParse(response, out end);

            while (!response.Equals("") && (!isEndDate || end.CompareTo(start) < 0))
            {
                Console.WriteLine("Enter a valid date (YYYY-MM-DD) that is greater than the start date:");
                response = Console.ReadLine();
                isEndDate = DateTime.TryParse(response, out end);
            }

            if (response.Equals("")) end = DateTime.Now;

            /*List<MovieModel> movies = _db.ReadByDate(start, end, max);
            
            if (movies != null)
            {
                foreach(MovieModel movie in movies)
                {
                    List<GenreModel> genres = _db.ReadMovieGenres(movie.Movie_ID);
                    if (genres != null) movie.Genres = genres;

                    Console.WriteLine(movie);
                }
            } else
            {
                Console.WriteLine("MySQL returned an empty result set (i.e. zero rows).");
            }*/

        }
    }
}
