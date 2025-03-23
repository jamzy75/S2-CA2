using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S2_CA2.Data;
using S2_CA2.Models;
using S2_CA2.Models.ViewModels;

namespace S2_CA2.Controllers
{
    [Authorize]
    public class BooksController : Controller
    {
        private readonly ApplicationDbContext _context;

        public BooksController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var books = _context.Books.ToList();
            return View(books);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(CreateBookVM book)
        {
            var author = await _context.Authors.FirstOrDefaultAsync(a => a.Id == book.AuthorId);
            if (author == null)
            {
                ModelState.AddModelError(nameof(CreateBookVM.AuthorId), "Author not found");
                return View(book);
            }

            if (!ModelState.IsValid) return View(book);

            _context.Books.Add(new Book
            {
                Author = author,
                Title = book.Title,
                Isbn = book.Isbn,
                Genre = book.Genre,
                PublishedDate = book.PublishedDate,
            });
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));

        }

        public IActionResult Edit(int id)
        {
            var book = _context.Books.Find(id);
            return book == null ? NotFound() : View(book);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Book book)
        {
            if (id != book.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(book);
        }

        public IActionResult Delete(int id)
        {
            var book = _context.Books.Find(id);
            return book == null ? NotFound() : View(book);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var book = _context.Books.Find(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
