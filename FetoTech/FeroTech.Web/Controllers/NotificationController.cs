using FeroTech.Infrastructure.Application.DTOs;
using FeroTech.Infrastructure.Application.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace FeroTech.Web.Controllers
{
    public class NotificationController : Controller
    {
        private readonly INotificationRepository _repo;

        public NotificationController(INotificationRepository repo)
        {
            _repo = repo;
        }

        // GET: /Notification
        public async Task<IActionResult> Index()
        {
            var notifications = await _repo.GetAllAsync();
            var ordered = notifications.OrderByDescending(n => n.CreatedAt);
            return View(ordered);
        }

        // POST: /Notification/MarkAsRead
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsRead(Guid id)
        {
            var dto = await _repo.GetByIdAsync(id);
            if (dto == null) return NotFound();

            dto.IsRead = true;
            await _repo.UpdateAsync(dto);

            // If request is AJAX, return simple JSON
            if (Request.IsAjaxRequest())
                return Json(new { success = true, id = id });

            return RedirectToAction(nameof(Index));
        }

        // POST: /Notification/MarkAsUnread
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> MarkAsUnread(Guid id)
        {
            var dto = await _repo.GetByIdAsync(id);
            if (dto == null) return NotFound();

            dto.IsRead = false;
            await _repo.UpdateAsync(dto);

            if (Request.IsAjaxRequest())
                return Json(new { success = true, id = id });

            return RedirectToAction(nameof(Index));
        }

        // POST: /Notification/Delete
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == default) return BadRequest();

            await _repo.DeleteAsync(id);

            if (Request.IsAjaxRequest())
                return Json(new { success = true, id = id });

            return RedirectToAction(nameof(Index));
        }
    }

    // small helper extension for detecting AJAX requests
    internal static class HttpRequestExtensions
    {
        public static bool IsAjaxRequest(this Microsoft.AspNetCore.Http.HttpRequest request)
        {
            if (request == null) throw new ArgumentNullException(nameof(request));
            if (request.Headers != null)
                return request.Headers["X-Requested-With"] == "XMLHttpRequest";
            return false;
        }
    }
}
