using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Context;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultSkillsComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultSkillsComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.SkillGroups
                .Where(g => g.IsActive)
                .Include(g => g.Skills.Where(s => s.IsActive).OrderByDescending(s => s.Level))
                .OrderBy(g => g.SkillGroupId)
                .ToList();
            return View(values);
        }
    }
}