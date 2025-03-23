using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using S2_CA2.Data;
using S2_CA2.Models;
using S2_CA2.Models.ViewModels;

namespace S2_CA2.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        private readonly UserManager<IdentityUser> _userManager;

        public ReviewsController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
        {
            _context = context;
            _userManager = userManager;
        }

        public IActionResult Index()
        {
            var reviews = _context.Reviews.Include(r => r.Book).ToList();
            return View(reviews);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(ReviewVM review)
        {
            var user = await _userManager.GetUserAsync(User);
            var book = await _context.Books.FindAsync(review.BookId);
            if (book == null)
            {
                ModelState.AddModelError(nameof(ReviewVM.BookId), "Could not find the specified book");
                return View(review);
            }

            if (ModelState.IsValid)
            {
                _context.Reviews.Add(new Review
                {
                    Book = book,
                    Content = review.Content,
                    Rating = review.Rating,
                    CreatedAt = DateTime.Now,
                    User = user!
                });
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }

            return View(review);
        }
    }
}