using LibraryService.Client;

namespace LibraryService.MVC.Models
{
    public class BookCategoryViewModel
    {
        public Book[] Books { get; set; } = null!;

        public SearchType SearchType { get; set; }

        public string SearchString { get; set; } = string.Empty;
    }
}
