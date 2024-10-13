namespace RepositotyApp.Data.Models
{
    public class Author
    {
        public int AuthorId { get; set; }
        public string? AuthorName { get; set; }  
        public DateTime DateOfBirth { get; set; }

        public ICollection<Book> Books { get; set; } = new List<Book>();
    }
}
