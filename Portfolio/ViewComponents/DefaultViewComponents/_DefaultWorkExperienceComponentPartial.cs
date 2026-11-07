using Microsoft.AspNetCore.Mvc;

namespace Portfolio.ViewComponents.DefaultViewComponents
{
    public class _DefaultWorkExperienceComponentPartial : ViewComponent
    {
        public IViewComponentResult Invoke()
        {
            return View();
        }
    }
}
