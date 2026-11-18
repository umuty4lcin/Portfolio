using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class SettingController : Controller
    {
        private readonly PortfolioContext _context;

        public SettingController(PortfolioContext context)
        {
            _context = context;
        }
        [HttpGet]
        public IActionResult SettingList()
        {
            var contact = _context.Contacts.FirstOrDefault() ?? new Contact();
            return View(contact);
        }
        [HttpPost]
        public IActionResult SettingList(Contact contact)
        {
            if (!ModelState.IsValid)
            {
                return View(contact);
            }

            var existing = _context.Contacts.Find(contact.ContactId);
            if(existing == null)
            {
                _context.Contacts.Add(contact);
            }
            else
            {
                existing.Description = contact.Description;
                existing.SubDescription = contact.SubDescription;
                existing.Email = contact.Email;
                existing.Location = contact.Location;
                existing.Linkedin = contact.Linkedin;
                existing.Github = contact.Github;
            }
            _context.SaveChanges();
            TempData["Success"] = "İletişim bilgileri güncellendi";
            return RedirectToAction("SettingList");
        }
    }
}
