using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultContactComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
