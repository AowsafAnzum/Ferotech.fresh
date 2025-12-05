using FeroTech.Infrastructure.Application.DTOs;
using FeroTech.Infrastructure.Application.Interfaces;
using FeroTech.Infrastructure.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


namespace FeroTech.Web.Controllers
{
    public class FineController : Controller
    {
        public readonly IFineRepository _repo;
        private readonly INotificationRepository _notificationRepo;


        public FineController(IFineRepository repo, INotificationRepository notificationRepo)
        {
            _repo = repo;
            _notificationRepo = notificationRepo;
        }

        public async Task<IActionResult> Index()
        {
            var fines = await _repo.GetAllAsync();
            return View(fines);
        }

        [HttpGet]
        public IActionResult Create() => View();

        
        [HttpPost]
        public async Task<IActionResult> Create(FineDto dto)
        {
            if (ModelState.IsValid)
            {
                var fine = new Fine
                {
                    FineId = Guid.NewGuid(),
                    MemberId = dto.MemberId,
                    IssueId = dto.IssueId,
                    FineAmount = dto.FineAmount,
                    FineDate = dto.FineDate == default ? DateTime.UtcNow : dto.FineDate,
                    PaymentStatus = dto.PaymentStatus ?? "Unpaid"
                };

                await _repo.AddAsync(fine);

                // ✅ Create notification
                var notification = new NotificationDto
                {
                    Title = "New Fine Added",
                    Message = $"A fine of amount {fine.FineAmount:C} was added for Member ID {fine.MemberId}.",
                    Category = "Fine",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                };

                await _notificationRepo.AddAsync(notification);

                return RedirectToAction(nameof(Index));
            }

            return View(dto);
        }

        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var fine = await _repo.GetByIdAsync(id);
            if (fine == null) return NotFound();

            var dto = new FineDto
            {
                FineId = fine.FineId,
                MemberId = fine.MemberId,
                IssueId = fine.IssueId,
                FineAmount = fine.FineAmount,
                FineDate = fine.FineDate,
                PaymentStatus = fine.PaymentStatus
            };
            return View(dto);
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(FineDto dto)
        {
            var existing = await _repo.GetByIdAsync(dto.FineId);
            if (existing == null) return NotFound();

            existing.FineAmount = dto.FineAmount;
            existing.PaymentStatus = dto.PaymentStatus;
            await _repo.UpdateAsync(existing);

            // ✅ Create notification
            var notification = new NotificationDto
            {
                Title = "Fine Updated",
                Message = $"Fine {existing.FineId} was updated (Amount: {existing.FineAmount}).",
                Category = "Fine",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await _notificationRepo.AddAsync(notification);

            return RedirectToAction(nameof(Index));
        }


        // GET: /Fine/Delete/{id}
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var fine = await _repo.GetByIdAsync(id);
            if (fine == null) return NotFound();

            var dto = new FineDto
            {
                FineId = fine.FineId,
                MemberId = fine.MemberId,
                IssueId = fine.IssueId,
                FineAmount = fine.FineAmount,
                FineDate = fine.FineDate,
                PaymentStatus = fine.PaymentStatus
            };

            return View(dto);
        }

        // POST: /Fine/Delete
        [HttpPost]
        public async Task<IActionResult> Delete(FineDto dto)
        {
            if (dto.FineId == default) return BadRequest();

            await _repo.DeleteAsync(dto.FineId);

            // ✅ Create notification
            var notification = new NotificationDto
            {
                Title = "Fine Deleted",
                Message = $"Fine record with ID {dto.FineId} has been deleted.",
                Category = "Fine",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await _notificationRepo.AddAsync(notification);

            return RedirectToAction(nameof(Index));
        }

    }
}

