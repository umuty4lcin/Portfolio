using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultPersonalWorkFlowComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
