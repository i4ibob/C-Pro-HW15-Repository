using RepositotyApp;
using RepositotyApp.Data;
using RepositotyApp.Data.Models;
using RepositotyApp.Repository;
using RepositotyApp.Repository.Interfaces;
using RepositotyApp.Services;

internal class Program
{
    static void Main(string[] args)
    {
        using (var context = new BooksContext())
        {
            IUnitOfWork unitOfWork = new UnitOfWork(context);
            LibraryService libraryService = new LibraryService(unitOfWork);

            bool running = true;

            while (running)
            {
                Console.WriteLine("\nМеню:");
                Console.WriteLine("1. Добавить книгу");
                Console.WriteLine("2. Обновить книгу");
                Console.WriteLine("3. Удалить книгу");
                Console.WriteLine("4. Показать все книги");
                Console.WriteLine("5. Выход");
                Console.Write("Выберите действие: ");
                string? choice = Console.ReadLine();

                switch (choice)
                {
                    case "1":
                        AddBook(libraryService, unitOfWork);
                        break;
                    case "2":
                        UpdateBook(libraryService, unitOfWork);
                        break;
                    case "3":
                        DeleteBook(libraryService, unitOfWork);
                        break;
                    case "4":
                        ShowAllBooks(libraryService);
                        break;
                    case "5":
                        running = false;
                        break;
                    default:
                        Console.WriteLine("Неверный выбор. Попробуйте снова.");
                        break;
                }
            }
        }
    }

    public static void AddBook(LibraryService libraryService, IUnitOfWork unitOfWork)
    {
        Console.WriteLine("Введите заголовок книги:");
        string title = Console.ReadLine();

        Console.WriteLine("Введите ID автора:");
        if (!int.TryParse(Console.ReadLine(), out int authorId) || !unitOfWork.Authors.GetAll().Any(a => a.AuthorId == authorId))
        {
            Console.WriteLine("Автор с таким ID не найден. Сначала создайте автора.");
            return;
        }

        Console.WriteLine("Введите ID жанра:");
        if (!int.TryParse(Console.ReadLine(), out int genreId) || !unitOfWork.Genres.GetAll().Any(g => g.GenreId == genreId))
        {
            Console.WriteLine("Жанр с таким ID не найден.");
            return;
        }

        Console.Write("Введите год публикации (DD.MM.YYYY): ");
        DateTime.TryParse(Console.ReadLine(), out DateTime publishedYear);
       
        var book = new Book
        {
            BookTitle = title,
            AuthorId = authorId,
            GenreId = genreId,
          PublishedYear = publishedYear
            };

        book.PublishedYear = publishedYear;

        unitOfWork.Books.Add(book);
        unitOfWork.Commit(); // Сохраните изменения
        Console.WriteLine("Книга успешно добавлена.");
    }

    static void UpdateBook(LibraryService libraryService, IUnitOfWork unitOfWork)
    {
        Console.Write("Введите ID книги для обновления: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId))
        {
            Console.WriteLine("Неверный ID.");
            return;
        }

        var book = libraryService.GetAllBooks().FirstOrDefault(b => b.BookId == bookId);
        if (book == null)
        {
            Console.WriteLine("Книга не найдена.");
            return;
        }

        Console.Write("Введите новое название книги: ");
        book.BookTitle = Console.ReadLine();

        Console.Write("Введите новый ID автора: ");
        if (int.TryParse(Console.ReadLine(), out int authorId))
        {
            book.AuthorId = authorId;
        }

        Console.Write("Введите новый ID жанра: ");
        if (int.TryParse(Console.ReadLine(), out int genreId))
        {
            book.GenreId = genreId;
        }

        Console.Write("Введите новый год публикации: ");
        if (DateTime.TryParse(Console.ReadLine(), out DateTime publishedYear))
        {
            book.PublishedYear = publishedYear;
        }

        libraryService.UpdateBook(book);
        unitOfWork.Commit();
        Console.WriteLine("Книга обновлена.");
    }

    static void DeleteBook(LibraryService libraryService, IUnitOfWork unitOfWork)
    {
        Console.Write("Введите ID книги для удаления: ");
        if (!int.TryParse(Console.ReadLine(), out int bookId))
        {
            Console.WriteLine("Неверный ID.");
            return;
        }

        libraryService.DeleteBook(bookId);
        unitOfWork.Commit();
        Console.WriteLine("Книга удалена.");
    }

    static void ShowAllBooks(LibraryService libraryService)
    {
        var books = libraryService.GetAllBooks();
        if (!books.Any())
        {
            Console.WriteLine("Книги не найдены.");
            return;
        }

        foreach (var book in books)
        {
            Console.WriteLine($"ID: {book.BookId}, Название: {book.BookTitle}, Автор: {book.AuthorId}, Жанр: {book.GenreId}, Год: {book.PublishedYear}");
        }
    }
}
