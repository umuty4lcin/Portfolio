using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultPersonalWorkFlowComponentPartial : ViewComponent
    {
        private readonly PortfolioContext _context;

        public _DefaultPersonalWorkFlowComponentPartial(PortfolioContext context)
        {
            _context = context;
        }

        public IViewComponentResult Invoke()
        {
            var values = _context.WorkflowSteps
                .Where(w => w.IsActive)
                .OrderBy(w => w.SortOrder)
                .ThenBy(w => w.WorkflowStepId)
                .ToList();
            return View(values);
        }
    }
}
