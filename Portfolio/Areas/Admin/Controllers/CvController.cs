using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CvController : Controller
    {
        private readonly PortfolioContext _context;
        private readonly IWebHostEnvironment _webHostEnvironment;

        public CvController(PortfolioContext context, IWebHostEnvironment webHostEnvironment)
        {
            _context = context;
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpGet]
        public IActionResult CvList()
        {
            var cv = _context.CvFiles.FirstOrDefault();
            return View(cv);   // kayıt yoksa null gider, view bunu karşılar
        }

        [HttpPost]
        public IActionResult UploadCv(IFormFile? cvFile)
        {
            if (cvFile == null || cvFile.Length == 0)
            {
                TempData["Error"] = "Lütfen bir PDF dosyası seçin.";
                return RedirectToAction("CvList");
            }

            var path = SavePdf(cvFile);
            if (path == null)
            {
                TempData["Error"] = "Sadece PDF yükleyebilirsiniz (en fazla 5 MB).";
                return RedirectToAction("CvList");
            }

            var existing = _context.CvFiles.FirstOrDefault();
            if (existing == null)
            {
                _context.CvFiles.Add(new CvFile
                {
                    FilePath = path,
                    OriginalFileName = Path.GetFileName(cvFile.FileName),
                    UploadedAt = DateTime.Now
                });
            }
            else
            {
                DeletePdf(existing.FilePath);   // eski dosyayı diskten sil
                existing.FilePath = path;
                existing.OriginalFileName = Path.GetFileName(cvFile.FileName);
                existing.UploadedAt = DateTime.Now;
            }
            _context.SaveChanges();
            TempData["Success"] = "CV başarıyla yüklendi.";
            return RedirectToAction("CvList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCv(int id)
        {
            var existing = _context.CvFiles.Find(id);
            if (existing == null)
            {
                return NotFound();
            }
            var filePath = existing.FilePath;
            _context.CvFiles.Remove(existing);
            _context.SaveChanges();
            DeletePdf(filePath);
            TempData["Success"] = "Yüklenen CV silindi. Ana sayfadaki buton sistemden otomatik üretilen CV'yi indirecek.";
            return RedirectToAction("CvList");
        }

        // PDF'i doğrulayıp diske kaydeder, geçersizse null döner
        private string? SavePdf(IFormFile file)
        {
            var ext = Path.GetExtension(file.FileName).ToLowerInvariant();
            if (ext != ".pdf" || file.Length > 5 * 1024 * 1024)
                return null;

            // İçerik gerçekten PDF mi? Dosya "%PDF" ile başlar.
            using (var reader = file.OpenReadStream())
            {
                var header = new byte[4];
                var read = reader.Read(header, 0, 4);
                if (read < 4 || System.Text.Encoding.ASCII.GetString(header) != "%PDF")
                    return null;
            }

            var fileName = Guid.NewGuid() + ".pdf";
            var folder = Path.Combine(_webHostEnvironment.WebRootPath, "files", "cv");
            Directory.CreateDirectory(folder);

            using var stream = new FileStream(Path.Combine(folder, fileName), FileMode.Create);
            file.CopyTo(stream);

            return "/files/cv/" + fileName;
        }

        private void DeletePdf(string? filePath)
        {
            if (string.IsNullOrEmpty(filePath) || !filePath.StartsWith("/files/cv/"))
                return;
            var fileName = Path.GetFileName(filePath);
            var fullPath = Path.Combine(_webHostEnvironment.WebRootPath, "files", "cv", fileName);

            if (System.IO.File.Exists(fullPath))
                System.IO.File.Delete(fullPath);
        }
    }
}
