using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultWorkExperienceComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultWorkExperienceComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Experiences
                .Where(e => e.IsActive)
                .OrderByDescending(e => e.StartDate)
                .ToList();
            return View(values);
        }
    }
}
