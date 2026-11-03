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
            var values = _context.Experiences.ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateExperience()
        {
            return View();
        }
        [HttpPost]
        public IActionResult CreateExperience(Experience experience)
        {
            _context.Add(experience);
            _context.SaveChanges();
            return RedirectToAction("ExperienceList");
        }
        public IActionResult DeleteExperience(int id)
        {
            var values = _context.Experiences.Find(id);
            _context.Experiences.Remove(values);
            _context.SaveChanges();
            return RedirectToAction("ExperienceList");
        }

        [HttpGet]
        public IActionResult UpdateExperience(int id)
        {
            var values = _context.Experiences.Find(id);
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateExperience(Experience experience)
        {
            _context.Experiences.Update(experience);
            _context.SaveChanges();
            return RedirectToAction("ExperienceList");
        }
    }
}
