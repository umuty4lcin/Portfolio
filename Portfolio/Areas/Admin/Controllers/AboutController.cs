using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class AboutController : Controller
    {
        private readonly PortfolioContext _context;

        public AboutController(PortfolioContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult AboutList()
        {
            var about = _context.Abouts.FirstOrDefault() ?? new About();
            return View(about);
        }
        [HttpPost] public IActionResult AboutList(About about)
        {
            if (!ModelState.IsValid)
            {
                return View(about);
            }

            var existing = _context.Abouts.Find(about.AboutId);
            if(existing == null)
            {
                _context.Abouts.Add(about);
            }
            else
            {
                existing.NameSurname = about.NameSurname;
                existing.JobTitle = about.JobTitle;
                existing.Location = about.Location;
                existing.Expert = about.Expert;
                existing.Email = about.Email;
                existing.Description = about.Description;
                existing.OpenToWork = about.OpenToWork;
            }
            _context.SaveChanges();
            TempData["Success"] = "Hakkımda bilgileri güncellendi";
            return RedirectToAction("AboutList");
        }
        
    }
}
