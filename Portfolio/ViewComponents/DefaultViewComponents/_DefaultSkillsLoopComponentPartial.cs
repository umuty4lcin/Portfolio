using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultSkillsLoopComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSkillsLoopComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.Skills
                .Where(s => s.IsActive && s.SkillGroup!.IsActive)
                .OrderByDescending(s => s.Level)
                .Take(12)
                .Select(s => s.Name)
                .ToList();
            return View(values);
        }
    }
}