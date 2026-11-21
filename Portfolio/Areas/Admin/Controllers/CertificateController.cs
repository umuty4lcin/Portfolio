using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class CertificateController : Controller
    {
        private readonly PortfolioContext _context;

        public CertificateController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult CertificateList()
        {
            var values = _context.Certificates.OrderByDescending(c => c.Year).ThenByDescending(c => c.CertificateId).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateCertificate()
        {
            return View(new Certificate());
        }

        [HttpPost]
        public IActionResult CreateCertificate(Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return View(certificate);
            }
            _context.Certificates.Add(certificate);
            _context.SaveChanges();
            TempData["Success"] = "Sertifika başarıyla eklendi.";
            return RedirectToAction("CertificateList");
        }

        [HttpGet]
        public IActionResult UpdateCertificate(int id)
        {
            var value = _context.Certificates.Find(id);
            if (value == null)
            {
                return NotFound();
            }
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateCertificate(Certificate certificate)
        {
            if (!ModelState.IsValid)
            {
                return View(certificate);
            }

            var existing = _context.Certificates.Find(certificate.CertificateId);
            if (existing == null)
            {
                return NotFound();
            }
            existing.Title = certificate.Title;
            existing.Issuer = certificate.Issuer;
            existing.Year = certificate.Year;
            existing.CertificateUrl = certificate.CertificateUrl;
            existing.IsActive = certificate.IsActive;

            _context.SaveChanges();
            TempData["Success"] = "Sertifika başarıyla güncellendi.";
            return RedirectToAction("CertificateList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteCertificate(int id)
        {
            var value = _context.Certificates.Find(id);
            if (value == null)
            {
                return NotFound();
            }
            _context.Certificates.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Sertifika başarıyla silindi.";
            return RedirectToAction("CertificateList");
        }
    }
}
