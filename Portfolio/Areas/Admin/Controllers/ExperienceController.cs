using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ExperienceController : Controller
    {
        private readonly PortfolioContext _context;
        public ExperienceController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult ExperienceList()
        {
            var values = _context.Experiences.OrderByDescending(e => e.StartDate).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateExperience()
        {
            return View(new Experience());
        }

        [HttpPost]
        public IActionResult CreateExperience(Experience experience)
        {
            ValidateDates(experience);

            if (!ModelState.IsValid)
            {
                return View(experience);
            }
            _context.Experiences.Add(experience);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim başarıyla eklendi.";
            return RedirectToAction("ExperienceList");
        }

        [HttpGet]
        public IActionResult UpdateExperience(int id)
        {
            var values = _context.Experiences.Find(id);
            if(values == null)
            {
                return NotFound();
            }
            return View(values);
        }

        [HttpPost]
        public IActionResult UpdateExperience(Experience experience)
        {
            ValidateDates(experience);
            if (!ModelState.IsValid)
            {
                return View(experience);
            }

            var existing = _context.Experiences.Find(experience.ExperienceId);
            if(existing == null)
            {
                return NotFound();
            }

            existing.Title = experience.Title;
            existing.CompanyName = experience.CompanyName;
            existing.Description = experience.Description;
            existing.SideTitle = experience.SideTitle;
            existing.StartDate = experience.StartDate;
            existing.EndDate = experience.EndDate;
            existing.WorkType = experience.WorkType;
            existing.Technologies = experience.Technologies;
            existing.IsActive = experience.IsActive;

            _context.SaveChanges();
            TempData["Success"] = "Deneyim başarıyla güncellendi.";
            return RedirectToAction("ExperienceList");
        }

        public IActionResult DeleteExperience(int id)
        {
            var value = _context.Experiences.Find(id);
            if(value == null)
            {
                return NotFound();
            }
            _context.Experiences.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Deneyim başarıyla silindi.";
            return RedirectToAction("ExperienceList");
        }

        //// Bitiş tarihi, başlangıçtan önce olamaz kontrolünü yapan yardımcı metot
        private void ValidateDates(Experience experience)
        {
            if(experience.EndDate.HasValue && experience.EndDate < experience.StartDate)
            {
                ModelState.AddModelError("EndDate", "Bitiş tarihi başlangıç tarihinden önce olamaz");
            }
        }
    }
}
