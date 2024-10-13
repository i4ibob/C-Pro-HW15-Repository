namespace RepositotyApp.Data.Models
{
    public class Book
    {
        public int BookId { get; set; }
        public string? BookTitle { get; set; }
        public int AuthorId { get; set; }
        public int GenreId { get; set; }
        public DateTime PublishedYear { get; set; }

        public Author Author { get; set; }
        public Genre Genre { get; set; }
    }
}
