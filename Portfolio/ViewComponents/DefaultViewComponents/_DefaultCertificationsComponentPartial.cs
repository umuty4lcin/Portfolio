using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultCertificationsComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultCertificationsComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Certificates
                .Where(c => c.IsActive)
                .OrderByDescending(c => c.Year)
                .ThenByDescending(c => c.CertificateId)
                .ToList();
            return View(values);
        }
    }
}
