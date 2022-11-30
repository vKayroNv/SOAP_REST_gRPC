using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Interfaces
{
    public interface ILibraryRepositoryService
    {
        IList<Book> GetAll();
        IList<Book> GetByTitle(string title);
        IList<Book> GetByAuthor(string authorName);
        IList<Book> GetByCategory(string category);
    }
}