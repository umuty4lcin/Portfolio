using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CategoryController : Controller
    {
        private readonly PortfolioContext _context;

        public CategoryController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult CategoryList()
        {
            var values = _context.Categories.ToList();
            return View(values);
        }
        public IActionResult CreateCategory()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateCategory(Category category)
        {
            _context.Add(category);
            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }
        public IActionResult DeleteCategory(int id)
        {
            var hasProjects = _context.Projects.Any(p => p.CategoryId == id);
            if (hasProjects)
            {
                TempData["Error"] = "Bu kategoriye ait projeler bulunmaktadır. Kategori silinemez.";
                return RedirectToAction("CategoryList");
            }
            var values = _context.Categories.Find(id);
            if (values == null)
            {
                return NotFound();
            }
            _context.Categories.Remove(values);
            _context.SaveChanges();
            TempData["Success"] = "Kategori silindi.";
            return RedirectToAction("CategoryList");
        }
        [HttpGet]
        public IActionResult UpdateCategory(int id)
        {
            var values = _context.Categories.Find(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateCategory (Category category)
        {
            _context.Categories.Update(category);
            _context.SaveChanges();
            return RedirectToAction("CategoryList");
        }
    }
}
