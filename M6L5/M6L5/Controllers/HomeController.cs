using M6L5.Core.Models;
using M6L5.Core.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace M6L5.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IBookService _bookService;
        public HomeController(ILogger<HomeController> logger, IBookService bookService)
        {
            _logger = logger;
            _bookService = bookService;
        }

        [HttpGet]
        [Route("Home/Book/{id:int}")]
        public IActionResult Book([FromRoute] int id)
        {
            Book book = _bookService.Get(id);
            return View(book);
        }
        [Authorize]
        public IActionResult Index()
        {
            var books = _bookService.Get();
            return View(books);
        }


        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public IActionResult Delete(int id)
        {
            _bookService.Delete(id);
            return RedirectToAction("Index");
        }

        public IActionResult Update(int? id)
        {
            if (id != null)
            {
                Book? book = _bookService.Get().FirstOrDefault(p => p.Id == id);
                if (book != null) return Content(book.Title);
            }
            return NotFound();
        }

        [HttpPost, ActionName("Update")]
        [ValidateAntiForgeryToken]
        public IActionResult Update(Book book)
        {
            _bookService.Update(book.Id, book.Description);
            return RedirectToAction("Index");
        }

        [HttpGet]
        public IActionResult AddItem()
        {
            return View();
        }
        [HttpPost]
        public IActionResult AddItem(int Id, string Title, DateTime ReleaseDate, string Image, string Description, string AuthorFullName, double Price, int BooksSold)
        {
            Book newBook = new Book { Id = Id, Title = Title, ReleaseDate = ReleaseDate, Image = Image, Description = Description, AuthorFullName = AuthorFullName, Price = Price, BooksSold = BooksSold };
            _bookService.Add(newBook);
            return Content("Successfully added");
        }


        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}