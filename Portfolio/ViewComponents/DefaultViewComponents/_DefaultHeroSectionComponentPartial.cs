using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultHeroSectionComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultHeroSectionComponentPartial(PortfolioContext context)
        {
            _context = context;
        }
        public IViewComponentResult Invoke()
        {
            var hero = _context.HeroSections.FirstOrDefault() ?? new HeroSection();
            ViewBag.Contact = _context.Contacts.FirstOrDefault() ?? new Contact();
            return View(hero);
        }
    }
}
