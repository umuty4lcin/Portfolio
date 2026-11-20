using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SkillController : Controller
    {
        private readonly PortfolioContext _context;

        public SkillController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult SkillList()
        {
            var values = _context.Skills.Include(s => s.SkillGroup).OrderBy(s => s.SkillGroupId).ThenByDescending(s => s.Level).ToList();
            return View(values);
        }
        [HttpGet]
        public IActionResult CreateSkill()
        {
            LoadGroups();
            return View(new Skill());
        }
        [HttpPost]
        public IActionResult CreateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                LoadGroups(skill.SkillGroupId);
                return View(skill);
            }
            _context.Skills.Add(skill);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek eklendi.";
            return RedirectToAction("SkillList");
        }
        [HttpGet]
        public IActionResult UpdateSkill(int id)
        {
            var skill = _context.Skills.Find(id);
            if (skill == null)
            {
                return NotFound();
            }
            LoadGroups(skill.SkillGroupId);
            return View(skill);
        }
        [HttpPost]
        public IActionResult UpdateSkill(Skill skill)
        {
            if (!ModelState.IsValid)
            {
                LoadGroups(skill.SkillGroupId);
                return View(skill);
            }

            var existing = _context.Skills.Find(skill.SkillId);
            if(existing == null)
            {
                return NotFound();
            }
            existing.Name = skill.Name;
            existing.Level = skill.Level;
            existing.IsActive = skill.IsActive;
            existing.SkillGroupId = skill.SkillGroupId;
            _context.SaveChanges();
            TempData["Success"] = "Yetenek bilgisi başarıyla güncellendi.";
            return RedirectToAction("SkillList");
        }
        public IActionResult DeleteSkill(int id)
        {
            var value = _context.Skills.Find(id);
            if(value == null)
            {
                return NotFound();
            }
            _context.Skills.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Yetenek bilgisi başarıyla silindi.";
            return RedirectToAction("SkillList");
        }

        //Grupları yüklemek için yardımcı metot
        private void LoadGroups(int? selectedId = null)
        {
            ViewBag.Groups = new SelectList(_context.SkillGroups.ToList(), "SkillGroupId", "Title", selectedId);
        }
    }
}
