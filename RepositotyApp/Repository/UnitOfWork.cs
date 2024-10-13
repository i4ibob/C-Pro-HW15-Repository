using Repository;
using RepositotyApp.Data;
using RepositotyApp.Data.Models;
using RepositotyApp.Repository.Interfaces;

namespace RepositotyApp.Repository
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly BooksContext _context;

        public IRepository<Book> Books { get; }
        public IRepository<Author> Authors { get; }
        public IRepository<Genre> Genres { get; }

        public UnitOfWork(BooksContext context)
        {
            _context = context;
            Books = new Repository<Book>(context);
            Authors = new Repository<Author>(context);
            Genres = new Repository<Genre>(context);
        }

        public void Commit()
        {
            _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
