using LibraryService.Client;
using LibraryService.MVC.Models;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace LibraryService.MVC.Controllers
{
    public class LibraryController : Controller
    {
        private readonly ILogger<LibraryController> _logger;

        public LibraryController(ILogger<LibraryController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index(SearchType searchType, string searchString, CancellationToken cancellationToken)
        {
            try
            {
                LibraryWebServiceSoapClient client = new(LibraryWebServiceSoapClient.EndpointConfiguration.LibraryWebServiceSoap);

                Book[] result = Array.Empty<Book>();
                switch (searchType)
                {
                    case SearchType.Title:
                        var titleResponse = await client.GetBooksByTitleAsync(searchString);
                        if (titleResponse != null)
                        {
                            result = titleResponse.Body.GetBooksByTitleResult;
                        }
                        break;
                    case SearchType.Author:
                        var authorResponse = await client.GetBooksByAuthorAsync(searchString);
                        if (authorResponse != null)
                        {
                            result = authorResponse.Body.GetBooksByAuthorResult;
                        }
                        break;
                    case SearchType.Category:
                        var categoryResponse = await client.GetBooksByCategoryAsync(searchString);
                        if (categoryResponse != null)
                        {
                            result = categoryResponse.Body.GetBooksByCategoryResult;
                        }
                        break;
                    default:
                        var allResponse = await client.GetAllAsync();
                        if (allResponse != null)
                        {
                            result = allResponse.Body.GetAllResult;
                        }
                        break;
                }

                return View(new BookCategoryViewModel
                {
                    Books = result
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Вызвано исключение");
            }

            return View(new BookCategoryViewModel
            {
                Books = Array.Empty<Book>()
            });
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}