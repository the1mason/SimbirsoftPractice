namespace LibraryApi.Models
{
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public HumanDto Author { get; set; }
        public GenreDto Genre { get; set; }
    }
}
