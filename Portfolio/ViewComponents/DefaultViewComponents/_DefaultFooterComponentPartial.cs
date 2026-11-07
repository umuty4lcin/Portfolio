using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
