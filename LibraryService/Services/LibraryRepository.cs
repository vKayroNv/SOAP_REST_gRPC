using LibraryService.Interfaces;
using LibraryService.Models;
using System.Collections.Generic;
using System.Linq;

namespace LibraryService.Services
{
    public class LibraryRepository : ILibraryRepositoryService
    {
        private readonly ILibraryDatabaseContextService _context;

        public LibraryRepository(ILibraryDatabaseContextService context)
        {
            _context = context;
        }

        public IList<Book> GetAll()
        {
            try
            {
                return _context.Books;
            }
            catch
            {
                return new List<Book>();
            }
        }

        public IList<Book> GetByAuthor(string authorName)
        {
            try
            {
                return _context.Books.Where(book =>
                    book.Authors.Where(author =>
                        author.Name.ToLower().Contains(authorName.ToLower())).Any()).ToList();
            }
            catch
            {
                return new List<Book>();
            }
        }

        public IList<Book> GetByCategory(string category)
        {
            try
            {
                return _context.Books.Where(book =>
                    book.Category.ToLower().Contains(category.ToLower())).ToList();
            }
            catch
            {
                return new List<Book>();
            }
        }

        public IList<Book> GetByTitle(string title)
        {
            try
            {
                return _context.Books.Where(book =>
                    book.Title.ToLower().Contains(title.ToLower())).ToList();
            }
            catch
            {
                return new List<Book>();
            }
        }
    }
}