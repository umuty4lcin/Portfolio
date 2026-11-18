using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Controllers
{
    public class DefaultController : Controller
    {
        private readonly PortfolioContext _context;

        public DefaultController(PortfolioContext context)
        {
            _context = context;
        }
        public IActionResult Index()
        {
            return View();
        }
        [HttpPost]
        public IActionResult SendMessage(Message message)
        {
            if (!ModelState.IsValid)
            {
                TempData["MessageError"] = "Lütfen formdaki alanları kontrol edin.";
                return RedirectToAction("Index", "Default", fragment: "contact");
            }
            message.SendDate = DateTime.Now;
            message.IsRead = false;
            _context.Messages.Add(message);
            _context.SaveChanges();
            TempData["MessageSuccess"] = "Mesajınız başarıyla iletildi. Teşekkür ederim!";
            return RedirectToAction("Index", "Default", fragment: "contact");
        }
    }
}
