namespace LibraryApi.Models
{
    /// <summary>
    /// 1.2.2
    /// </summary>
    public class BookDto
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public int AuthorId { get; set; }
        public GenreDto Genre { get; set; }
    }
}
