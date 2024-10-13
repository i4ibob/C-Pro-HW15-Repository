using Microsoft.EntityFrameworkCore;
using RepositotyApp.Data;
using RepositotyApp.Repository.Interfaces;
using System.Collections.Generic;
using System.Linq;

namespace Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly BooksContext _context;
        private readonly DbSet<T> _dbSet;

        public Repository(BooksContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public T GetById(int id) => _dbSet.Find(id);

        public IEnumerable<T> GetAll() => _dbSet.ToList();

        public void Add(T entity) => _dbSet.Add(entity);

        public void Update(T entity) => _dbSet.Update(entity);

        public void Delete(T entity) => _dbSet.Remove(entity);
    }
}
