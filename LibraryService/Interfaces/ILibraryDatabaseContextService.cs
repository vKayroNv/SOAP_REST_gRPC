using LibraryService.Models;
using System.Collections.Generic;

namespace LibraryService.Interfaces
{
    public interface ILibraryDatabaseContextService
    {
        IList<Book> Books { get; }
    }
}