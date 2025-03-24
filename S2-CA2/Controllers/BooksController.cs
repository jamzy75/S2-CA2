using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
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

        private async Task<SelectList> GetAuthorsAsSelectList() => new(
            await _context.Authors
                .AsNoTracking()
                .ToListAsync(),
            nameof(Author.Id),
            nameof(Author.Name)
        );


        public IActionResult Index()
        {
            var books = _context.Books
                .AsNoTracking()
                .Include(b => b.Author)
                .ToList();
            return View(books);
        }

        public async Task<IActionResult> Create()
        {
            var model = new BookVM { AuthorsList = await GetAuthorsAsSelectList() };
            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(BookVM book)
        {
            book.AuthorsList = await GetAuthorsAsSelectList();
            var author = await _context.Authors.FindAsync(book.AuthorId);
            if (author == null)
            {
                ModelState.AddModelError(nameof(BookVM.AuthorId), "Author not found");
                return View(book);
            }

            var ctx = new ValidationContext(book);
            var validationResults = new List<ValidationResult>();

            if (Validator.TryValidateObject(book, ctx, validationResults)) return View(book);

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

        public async Task<IActionResult> Edit(int id)
        {
            var book = await _context.Books
                .Include(b => b.Author)
                .FirstOrDefaultAsync(b => b.Id == id);

            if (book == null)
            {
                return NotFound();
            }

            var model = new BookVM(book, await GetAuthorsAsSelectList())
            {
                AuthorId = book.Author.Id
            };

            return View(model);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, BookVM book)
        {
            if (id != book.Id) return NotFound();

            Book newBookData = book;
            var author = await _context.Authors.FindAsync(book.AuthorId);

            if (author == null)
            {
                ModelState.AddModelError(nameof(BookVM.AuthorId), "Author not found");
                return View(book);
            }

            book.Author = author;
            newBookData.Author = author;

            var ctx = new ValidationContext(book);
            var validationResults = new List<ValidationResult>();


            if (Validator.TryValidateObject(book, ctx, validationResults))
            {
                _context.Update(book);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            book.AuthorsList = await GetAuthorsAsSelectList();

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
            var book = await _context.Books.FindAsync(id);
            if (book != null)
            {
                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
            }

            return RedirectToAction(nameof(Index));
        }
    }
}