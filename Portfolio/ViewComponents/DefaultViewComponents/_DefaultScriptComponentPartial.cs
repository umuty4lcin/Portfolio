using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultScriptComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
