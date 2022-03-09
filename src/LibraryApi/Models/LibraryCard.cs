namespace LibraryApi.Models
{
    /// <summary>
    /// 2.1.1
    /// </summary>
    public class LibraryCard
    {
        public HumanDto Human { get; set; }
        public System.DateTimeOffset DateRecieved { get; set; }
        public BookDto Book { get; set; }
    }
}
