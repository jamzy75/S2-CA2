using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using S2_CA2.Data;
using S2_CA2.Models;
using System.Linq;
using System.Threading.Tasks;

namespace S2_CA2.Controllers
{
    [Authorize]
    public class ReviewsController : Controller
    {
        private readonly ApplicationDbContext _context;

        public ReviewsController(ApplicationDbContext context)
        {
            _context = context;
        }

        public IActionResult Index()
        {
            var reviews = _context.Reviews.ToList();
            return View(reviews);
        }

        public IActionResult Create() => View();

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(Review review)
        {
            if (ModelState.IsValid)
            {
                _context.Reviews.Add(review);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(review);
        }
    }
}
