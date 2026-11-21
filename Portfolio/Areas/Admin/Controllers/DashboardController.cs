using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Areas.Admin.Models;
using Microsoft.EntityFrameworkCore;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class DashboardController : Controller
    {
        private readonly PortfolioContext _context;

        public DashboardController(PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            var vm = new DashboardViewModel
            {
                TotalMessages = _context.Messages.Count(),
                UnreadMessages = _context.Messages.Count(m => !m.IsRead),
                TotalProjects = _context.Projects.Count(),
                ActiveProjects = _context.Projects.Count(p => p.IsActive),
                TotalCategories = _context.Categories.Count(),

                LastMessages = _context.Messages
                    .OrderByDescending(m => m.SendDate)
                    .Take(5)
                    .ToList(),

                LastProjects = _context.Projects
                    .Include(p => p.Category)
                    .OrderByDescending(p => p.ProjectId)
                    .Take(3)
                    .ToList(),

                CategoryStats = _context.Projects
                    .GroupBy(p => p.Category.CategoryName)
                    .Select(g => new CategoryStat
                    {
                        CategoryName = g.Key,
                        ProjectCount = g.Count()
                    })
                    .ToList()
            };
            return View(vm);
        }
    }
}
