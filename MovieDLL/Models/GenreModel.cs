namespace MovieDLL.Models
{
    public class GenreModel
    {
        public int Genre_ID { get; set; }
        public string Genre { get; set; }
        public override string ToString()
        {
            return $"{Genre_ID}\t{Genre}";
        }
    }
}
