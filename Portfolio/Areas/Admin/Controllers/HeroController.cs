using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class HeroController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public HeroController(PortfolioContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult HeroList()
        {
            var hero = _context.HeroSections.FirstOrDefault() ?? new HeroSection();
            return View(hero);
        }

        [HttpPost]
        public IActionResult HeroList(HeroSection hero, IFormFile? heroImage)
        {
            if (!ModelState.IsValid)
            {
                return View(hero);
            }

            var existing = _context.HeroSections.Find(hero.HeroSectionId);

            string? newImagePath = null;
            if (heroImage != null && heroImage.Length > 0)
            {
                newImagePath = SaveImage(heroImage);
                if (newImagePath == null)
                {
                    ViewBag.ImageError = "Sadece jpg, jpeg veya png yükleyebilirsiniz (en fazla 2 MB).";
                    hero.ImageUrl = existing?.ImageUrl;
                    return View(hero);
                }
            }

            if (existing == null)
            {
                hero.ImageUrl = newImagePath;
                _context.HeroSections.Add(hero);
            }
            else
            {
                existing.Title = hero.Title;
                existing.SubTitle = hero.SubTitle;
                existing.Description = hero.Description;
                if (newImagePath != null)
                {
                    DeleteImage(existing.ImageUrl);
                    existing.ImageUrl = newImagePath;
                }
            }
            _context.SaveChanges();
            TempData["Success"] = "Hero bölümü güncellendi.";
            return RedirectToAction("HeroList");
        }

        // Görsel yükleme yardımcı metodu
        private string? SaveImage(IFormFile file)
        {
            var allowed = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if (!allowed.Contains(ext) || file.Length > 2 * 1024 * 1024)
                return null;

            var fileName = Guid.NewGuid() + ext;
            var folder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "hero");
            Directory.CreateDirectory(folder);

            using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
            file.CopyTo(stream);

            return "/images/hero/" + fileName;
        }

        // Sadece bu klasörde yüklenmiş görselleri siler (dış adresleri ve eski görselleri dokunmaz)
        private void DeleteImage(string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl) || !imageUrl.StartsWith("/images/hero/"))
                return;
            var fileName = Path.GetFileName(imageUrl);
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "hero", fileName);

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);
        }
    }
}
