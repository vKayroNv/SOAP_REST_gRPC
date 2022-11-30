using System.ComponentModel.DataAnnotations;

namespace LibraryService.MVC.Models
{
    public enum SearchType
    {
        [Display(Name = "")]
        None,
        [Display(Name = "Заголовок")]
        Title,
        [Display(Name = "Автор")]
        Author,
        [Display(Name = "Категория")]
        Category
    }
}
