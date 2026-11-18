using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultEducationComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultEducationComponentPartial(PortfolioContext? context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Educations
                .Where(e => e.IsActive)
                .OrderBy(e => e.EducationId)
                .ToList();
            return View(values);
        }
    }
}
