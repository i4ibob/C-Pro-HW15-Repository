using RepositotyApp.Data.Models;
using RepositotyApp.Repository.Interfaces;
using System.Collections.Generic;

namespace RepositotyApp.Services
{
    public class LibraryService
    {
        private readonly IUnitOfWork _unitOfWork;

        public LibraryService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public void AddBook(Book book)
        {
            _unitOfWork.Books.Add(book);
        }

        public void UpdateBook(Book book)
        {
            _unitOfWork.Books.Update(book);
        }

        public void DeleteBook(int bookId)
        {
            var book = _unitOfWork.Books.GetById(bookId);
            if (book != null)
            {
                _unitOfWork.Books.Delete(book);
            }
        }

        public IEnumerable<Book> GetAllBooks()
        {
            return _unitOfWork.Books.GetAll();
        }
    }
}
