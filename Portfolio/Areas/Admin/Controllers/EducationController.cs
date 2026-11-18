using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class EducationController : Controller
    {
        private readonly PortfolioContext _context;

        public EducationController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult EducationList()
        {
            var values = _context.Educations.OrderBy(e => e.EducationId).ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateEducation()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateEducation(Education education)
        {
            if (!ModelState.IsValid)
            {
                return View(education);
            }
            _context.Educations.Add(education);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim bilgisi başarıyla eklendi.";
            return RedirectToAction("EducationList");
        }
        [HttpGet]
        public IActionResult UpdateEducation(int id)
        {
            var values = _context.Educations.Find(id);
            if(values == null)
            {
                return NotFound();
            }
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateEducation(Education education)
        {
            if (!ModelState.IsValid)
            {
                return View(education);
            }

            var existing = _context.Educations.Find(education.EducationId);
            if (existing == null)
            {
                return NotFound();
            }
            existing.Title = education.Title;
            existing.SubTitle = education.SubTitle;
            existing.Description = education.Description;
            existing.Duration = education.Duration;
            existing.IsActive = education.IsActive;
            _context.SaveChanges();
            TempData["Success"] = "Eğitim bilgisi başarıyla güncellendi.";
            return RedirectToAction("EducationList");
        }
        public IActionResult DeleteEducation(int id)
        {
            var values = _context.Educations.Find(id);
            if(values == null)
            {
                return NotFound();
            }
            _context.Educations.Remove(values);
            _context.SaveChanges();
            TempData["Success"] = "Eğitim bilgisi başarıyla silindi.";
            return RedirectToAction("EducationList");
        }
    }
}
