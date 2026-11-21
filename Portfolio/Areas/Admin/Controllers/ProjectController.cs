using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class ProjectController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProjectController(PortfolioContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        public IActionResult ProjectList()
        {
            var values = _context.Projects.Include(p => p.Category).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateProject()
        {
            LoadCategories();
            return View(new Project());
        }
        [HttpPost]
        public IActionResult CreateProject(Project project, IFormFile? coverImage)
        {
            if (!ModelState.IsValid)
            {
                LoadCategories(project.CategoryId);
                return View(project);
            }

            if (coverImage == null || coverImage.Length == 0)
            {
                ViewBag.ImageError = "Lütfen bir görsel seçin.";
                LoadCategories(project.CategoryId);
                return View(project);
            }

            var path = SaveImage(coverImage);
            if (path == null)
            {
                ViewBag.ImageError = "Sadece jpg, jpeg veya png yükleyebilirsiniz (en fazla 2 MB).";
                LoadCategories(project.CategoryId);
                return View(project);
            }

            project.CoverImageUrl = path;
            _context.Projects.Add(project);
            _context.SaveChanges();
            TempData["Success"] = "Proje başarıyla eklendi.";
            return RedirectToAction("ProjectList");
        }
        [HttpGet]
        public IActionResult UpdateProject(int id)
        {
            var project = _context.Projects.Find(id);
            if (project == null)
            {
                return NotFound();
            }
            LoadCategories(project.CategoryId);
            return View(project);
        }
        [HttpPost]
        [HttpPost]
        public IActionResult UpdateProject(Project project, IFormFile? coverImage)
        {
            var existingProject = _context.Projects.Find(project.ProjectId);
            if (existingProject == null)
            {
                return NotFound();
            }

            if (!ModelState.IsValid)
            {
                project.CoverImageUrl = existingProject.CoverImageUrl;
                LoadCategories(project.CategoryId);
                return View(project);
            }

            if (coverImage != null && coverImage.Length > 0)
            {
                var path = SaveImage(coverImage);
                if (path == null)
                {
                    ViewBag.ImageError = "Sadece jpg, jpeg veya png yükleyebilirsiniz (en fazla 2 MB).";
                    project.CoverImageUrl = existingProject.CoverImageUrl;
                    LoadCategories(project.CategoryId);
                    return View(project);
                }
                DeleteImage(existingProject.CoverImageUrl);
                existingProject.CoverImageUrl = path;
            }

            existingProject.Title = project.Title;
            existingProject.Description = project.Description;
            existingProject.CategoryId = project.CategoryId;
            existingProject.GithubUrl = project.GithubUrl;
            existingProject.IsActive = project.IsActive;
            _context.SaveChanges();
            TempData["Success"] = "Proje başarıyla güncellendi.";
            return RedirectToAction("ProjectList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteProject(int id)
        {
            var values = _context.Projects.Find(id);
            if (values == null)
            {
                return NotFound();
            }
            var imageUrl = values.CoverImageUrl;
            _context.Projects.Remove(values);
            _context.SaveChanges();
            DeleteImage(imageUrl);
            TempData["Success"] = "Proje başarıyla silindi.";
            return RedirectToAction("ProjectList");
        }

        //Kategori ekleme ve güncelleme işlemleri için kategori listesini yükleyen bir yardımcı metot
        private void LoadCategories(int? selectedId = null)
        {
            ViewBag.Categories = new SelectList(_context.Categories.ToList(), "CategoryId", "CategoryName", selectedId);
        }

        // Görsel ekleme ve güncelleme işlemleri için yardımcı metot
        private string? SaveImage(IFormFile file)
        {
            var allowed = new[] { ".jpg", ".jpeg", ".png" };
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();

            if(!allowed.Contains(ext) || file.Length > 2 * 1024 * 1024)
                return null;

            var fileName = Guid.NewGuid() + ext;
            var folder = Path.Combine(_webHostEnvironment.WebRootPath, "images", "projects");
            Directory.CreateDirectory(folder);

            using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
            file.CopyTo(stream);

            return "/images/projects/" + fileName;
        }
        private void DeleteImage(string? imageUrl)
        {
            if (string.IsNullOrEmpty(imageUrl) || !imageUrl.StartsWith("/images/projects/"))
                return;
            var fileName = Path.GetFileName(imageUrl);
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "images", "projects", fileName);

            if(System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);
        }
    }
}
