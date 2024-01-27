namespace MoviesApi.Models
{
    public class Movie
    {
        public int ID { get; set; }
        [MaxLength(50)]
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        [MaxLength(2500)]
        public string Storyline { get; set; }
        public byte[] Poster { get; set; }
        public byte GenreID { get; set; }
        public Genre Genre { get; set; }

    }
}
