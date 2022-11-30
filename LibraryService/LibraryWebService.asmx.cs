using LibraryService.Interfaces;
using LibraryService.Models;
using LibraryService.Services;
using System.Linq;
using System.Web.Services;

namespace LibraryService
{
    [WebService(Namespace = "http://tempuri.org/")]
    [WebServiceBinding(ConformsTo = WsiProfiles.BasicProfile1_1)]
    [System.ComponentModel.ToolboxItem(false)]
    public class LibraryWebService : System.Web.Services.WebService
    {
        private readonly ILibraryRepositoryService _libraryRepository;

        public LibraryWebService()
        {
            _libraryRepository = new LibraryRepository(new DatabaseContext());
        }

        [WebMethod]
        public Book[] GetAll()
        {
            return _libraryRepository.GetAll().ToArray();
        }

        [WebMethod]
        public Book[] GetBooksByTitle(string title)
        {
            return _libraryRepository.GetByTitle(title).ToArray();
        }

        [WebMethod]
        public Book[] GetBooksByAuthor(string author)
        {
            return _libraryRepository.GetByAuthor(author).ToArray();
        }

        [WebMethod]
        public Book[] GetBooksByCategory(string category)
        {
            return _libraryRepository.GetByCategory(category).ToArray();
        }
    }
}
