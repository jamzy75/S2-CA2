using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S2_CA2.Data;
using S2_CA2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace S2_CA2.Controllers
{
    [Authorize]
    public class AuthorsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AuthorsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var authors = _context.Authors.ToList();
            return View(authors);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Author author)
        {
            if (ModelState.IsValid)
            {
                _context.Authors.Add(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public IActionResult Edit(int id)
        {
            var author = _context.Authors.Find(id);
            return author == null ? NotFound() : View(author);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, Author author)
        {
            if (id != author.Id) return NotFound();

            if (ModelState.IsValid)
            {
                _context.Update(author);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(author);
        }

        public IActionResult Delete(int id)
        {
            var author = _context.Authors.Find(id);
            return author == null ? NotFound() : View(author);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var author = _context.Authors.Find(id);
            if (author != null)
            {
                _context.Authors.Remove(author);
                await _context.SaveChangesAsync();
            }
            return RedirectToAction(nameof(Index));
        }
    }
}
