 using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillGroupController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillGroupController(PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult SkillGroupList()
        {
            var values = _context.SkillGroups.Include(g => g.Skills).OrderBy(g => g.SkillGroupId).ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateSkillGroup()
        {
            return View(new SkillGroup());
        }
        [HttpPost]
        public IActionResult CreateSkillGroup(SkillGroup skillGroup)
        {
            if (!ModelState.IsValid)
            {
                return View(skillGroup);
            }
            _context.SkillGroups.Add(skillGroup);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek grubu eklendi.";
            return RedirectToAction("SkillGroupList");
        }
        [HttpGet]
        public IActionResult UpdateSkillGroup(int id)
        {
            var values = _context.SkillGroups.Find(id);
            if (values == null)
            {
                return NotFound();
            }
            return View(values);
        }
        [HttpPost]
        public IActionResult UpdateSkillGroup(SkillGroup skillGroup)
        {
            if (!ModelState.IsValid)
            {
                return View(skillGroup);
            }

            var existing = _context.SkillGroups.Find(skillGroup.SkillGroupId);
            if(existing == null)
            {
                return NotFound();
            }
            existing.Title = skillGroup.Title;
            existing.Description = skillGroup.Description;
            existing.IconClass = skillGroup.IconClass;
            existing.ColorClass = skillGroup.ColorClass;
            existing.IsActive = skillGroup.IsActive;

            _context.SaveChanges();
            TempData["Success"] = "Yetenek grubu bilgisi başarıyla güncellendi.";
            return RedirectToAction("SkillGroupList");
        }
        public IActionResult DeleteSkillGroup(int id)
        {
            var hasSkills = _context.Skills.Any(p => p.SkillGroupId == id);
            if (hasSkills)
            {
                TempData["Error"] = "Bu yetenek grubuna ait yetenekler bulunmaktadır. Yetenek grubu silinemez.";
                return RedirectToAction("SkillGroupList");
            }

            var value = _context.SkillGroups.Find(id);
            if(value == null)
            {
                return NotFound();
            }
            _context.SkillGroups.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek grubu bilgisi başarıyla silindi.";
            return RedirectToAction("SkillGroupList");
        }
    }
}
