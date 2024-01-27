namespace MoviesApi.DTO
{
    public class MovieDetailsDTO
    {
        public int ID { get; set; }
        public string Title { get; set; }
        public int Year { get; set; }
        public double Rate { get; set; }
        public string Storyline { get; set; }
        public byte[] Poster { get; set; }
        public byte GenreID { get; set; }
        public string GenreName { get; set; }
    }
}
