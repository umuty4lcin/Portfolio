using Microsoft.AspNetCore.Mvc;
using Portfolio.Context;
using Portfolio.Entities;

namespace Portfolio.Areas.Admin.Controllers
{
    [Area("Admin")]
    public class MessageController : Controller
    {
        private readonly PortfolioContext _context;

        public MessageController(PortfolioContext context)
        {
            _context = context;
        }

        public IActionResult MessageList(string? search, string? sort, string? status)
        {
            var query = _context.Messages.AsQueryable();

            if (!string.IsNullOrWhiteSpace(search))
                query = query.Where(m => m.NameSurname.Contains(search) || m.Subject.Contains(search));

            if (status == "unread")
                query = query.Where(m => !m.IsRead);
            else if (status == "read")
                query = query.Where(m => m.IsRead);

            query = sort == "oldest"
                ? query.OrderBy(m => m.SendDate)
                : query.OrderByDescending(m => m.SendDate);

            ViewBag.UnreadCount = _context.Messages.Count(m => !m.IsRead);
            ViewBag.Search = search;
            ViewBag.Sort = sort;
            ViewBag.Status = status;

            return View(query.ToList());
        }
        [HttpPost]
        public IActionResult MessageDetail(int id)
        {
            var message = _context.Messages.Find(id);
            if (message == null)
            {
                return NotFound();
            }

            if (!message.IsRead)
            {
                message.IsRead = true;
                _context.SaveChanges();
            }

            return Json(new
            {
                id = message.MessageId,
                nameSurname = message.NameSurname,
                email = message.Email,
                subject = message.Subject,
                messageDetail = message.MessageDetail,
                sendDate = message.SendDate.ToString("dd.MM.yyyy HH:mm")
            });
        }
        public IActionResult DeleteMessage(int id)
        {
            var value = _context.Messages.Find(id);
            if (value == null)
            {
                return NotFound();
            }

            _context.Messages.Remove(value);
            _context.SaveChanges();
            return RedirectToAction("MessageList");
        }
    }
}
