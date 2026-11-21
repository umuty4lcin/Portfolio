using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Context;
using Portfolio.Entities;
using Portfolio.Services;

namespace Portfolio.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public DefaultController(PortfolioContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageError"] = "Lütfen formdaki alanları kontrol edin.";
                return RedirectToAction("Index", "Default", fragment: "contact");
            }
            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Mesajınız başarıyla iletildi. Teşekkür ederim!";
            return RedirectToAction("Index", "Default", fragment: "contact");
        }
        public IActionResult DownloadCv()
        {
            // 1) Admin panelinden yüklenmiş PDF varsa onu ver
            var cv = _context.CvFiles.FirstOrDefault();
            if (cv != null)
            {
                var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "files", "cv", Path.GetFileName(cv.FilePath));
                if (System.IO.File.Exists(fullPath))
                {
                    return PhysicalFile(fullPath, "application/pdf", cv.OriginalFileName);
                }
            }

            // 2) Yüklenmemişse sitedeki bilgilerden otomatik üret
            var about = _context.Abouts.FirstOrDefault();
            if (about == null)
            {
                return NotFound();
            }

            var contact = _context.Contacts.FirstOrDefault();
            var educations = _context.Educations.Where(e => e.IsActive).OrderBy(e => e.EducationId).ToList();
            var experiences = _context.Experiences.Where(e => e.IsActive).OrderByDescending(e => e.StartDate).ToList();
            var groups = _context.SkillGroups
                .Where(g => g.IsActive)
                .Include(g => g.Skills.Where(s => s.IsActive).OrderByDescending(s => s.Level))
                .OrderBy(g => g.SkillGroupId)
                .ToList();
            var projects = _context.Projects
                .Where(p => p.IsActive)
                .Include(p => p.Category)
                .OrderByDescending(p => p.ProjectId)
                .ToList();

            var pdf = CvPdfBuilder.Build(about, contact, educations, experiences, groups, projects);
            var fileName = about.NameSurname.Replace(" ", "-") + "-CV.pdf";
            return File(pdf, "application/pdf", fileName);
        }
        public IActionResult StatusPage(int code = 404)
        {
            ViewBag.Code = code;
            return View();
        }

        public IActionResult Error()
        {
            ViewBag.Code = 500;
            return View("StatusPage");
        }
    }
}
