using Microsoft.AspNetCore.Mvc;

namespace Portfolio.Areas.Admin.ViewComponents.AdminLayoutViewComponents
{
    public class _AdminLayoutFooterComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
