using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Context;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultProjectComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultProjectComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var values = _context.Projects.Include(p => p.Category).Where(p => p.IsActive).ToList();
            return View(values);
        }
    }
}
