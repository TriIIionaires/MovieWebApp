namespace MovieDLL.Models
{
    public class MovieModel
    {
        public enum Ratings
        {
            G,
            PG,
            PG13,
            R
        }

        public int ID { get; set; }
        public int Movie_ID { get; set; }
        public string IMDB_ID { get; set; }
        public string Title { get; set; }
        public List<GenreModel> Genres { get; set; }
        public DateTime Release_Date { get; set; }
        public int RunTime { get; set; }
        public Ratings MPAA_Rating { get; set; }
        public double Rating { get; set; }
        public int Votes { get; set; }
        public string Tagline { get; set; }
        public string Description { get; set; }
        public string Homepage { get; set; }
        public string PosterURL { get; set; }

        public override string ToString()
        {
            string output = $"\n{Title}\n";
            
            if (Genres != null)
            {
                foreach (GenreModel genre in Genres)
                {
                    output += $"{genre.Genre}, ";
                }

                output = output.Substring(0, output.Length - 2); // Omits last comma and space 

            } else
            {
                output += "Genre Unavailable";
            }
            
            output += $"\n{Release_Date.ToShortDateString()} \u00B7 {RunTime / 60}h {RunTime % 60}m \u00B7 {MPAA_Rating} \u00B7 {Rating}/10 ({Votes} votes)\n{Description}\nID: {ID} | Movie ID: {Movie_ID} | IMDB ID: {IMDB_ID}";

            return output;
        }
    }
}
