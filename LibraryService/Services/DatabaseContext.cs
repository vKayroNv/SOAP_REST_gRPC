using LibraryService.Interfaces;
using LibraryService.Models;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;

namespace LibraryService.Services
{
    public class DatabaseContext : ILibraryDatabaseContextService
    {
        private IList<Book> _books;

        public IList<Book> Books => _books;

        public DatabaseContext()
        {
            Initialize();
        }

        private void Initialize()
        {
            var path = Path.Combine(AppContext.BaseDirectory, "books.json");
            var data = File.ReadAllText(path);

            _books = JsonConvert.DeserializeObject<IList<Book>>(data);
        }
    }
}