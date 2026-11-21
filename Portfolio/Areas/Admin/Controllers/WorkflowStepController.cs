using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class WorkflowStepController : Controller
    {
        private readonly PortfolioContext _context;

        public WorkflowStepController(PortfolioContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult WorkflowStepList()
        {
            var values = _context.WorkflowSteps.OrderBy(w => w.SortOrder).ThenBy(w => w.WorkflowStepId).ToList();
            return View(values);
        }

        [HttpGet]
        public IActionResult CreateWorkflowStep()
        {
            // Yeni adım varsayılan olarak listenin sonuna eklenir
            var nextOrder = _context.WorkflowSteps.Any() ? _context.WorkflowSteps.Max(w => w.SortOrder) + 1 : 1;
            return View(new WorkflowStep { SortOrder = Math.Min(nextOrder, 99) });
        }

        [HttpPost]
        public IActionResult CreateWorkflowStep(WorkflowStep step)
        {
            if (!ModelState.IsValid)
            {
                return View(step);
            }
            _context.WorkflowSteps.Add(step);
            _context.SaveChanges();
            TempData["Success"] = "Çalışma adımı başarıyla eklendi.";
            return RedirectToAction("WorkflowStepList");
        }

        [HttpGet]
        public IActionResult UpdateWorkflowStep(int id)
        {
            var value = _context.WorkflowSteps.Find(id);
            if (value == null)
            {
                return NotFound();
            }
            return View(value);
        }

        [HttpPost]
        public IActionResult UpdateWorkflowStep(WorkflowStep step)
        {
            if (!ModelState.IsValid)
            {
                return View(step);
            }

            var existing = _context.WorkflowSteps.Find(step.WorkflowStepId);
            if (existing == null)
            {
                return NotFound();
            }
            existing.Title = step.Title;
            existing.Description = step.Description;
            existing.IconClass = step.IconClass;
            existing.SortOrder = step.SortOrder;
            existing.IsActive = step.IsActive;

            _context.SaveChanges();
            TempData["Success"] = "Çalışma adımı başarıyla güncellendi.";
            return RedirectToAction("WorkflowStepList");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public IActionResult DeleteWorkflowStep(int id)
        {
            var value = _context.WorkflowSteps.Find(id);
            if (value == null)
            {
                return NotFound();
            }
            _context.WorkflowSteps.Remove(value);
            _context.SaveChanges();
            TempData["Success"] = "Çalışma adımı başarıyla silindi.";
            return RedirectToAction("WorkflowStepList");
        }
    }
}
