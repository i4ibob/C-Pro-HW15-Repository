using RepositotyApp.Data.Models;

namespace RepositotyApp.Repository.Interfaces
{
    public interface IUnitOfWork : System.IDisposable
    {
        IRepository<Book> Books { get; }
        IRepository<Author> Authors { get; }
        IRepository<Genre> Genres { get; }
        void Commit();
    }
}
