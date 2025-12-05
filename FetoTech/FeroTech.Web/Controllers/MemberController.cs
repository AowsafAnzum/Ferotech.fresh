using FeroTech.Infrastructure.Application.DTOs;
using FeroTech.Infrastructure.Application.Interfaces;
using FeroTech.Infrastructure.Domain.Entities;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
namespace FeroTech.Web.Controllers
{
    public class MemberController : Controller
    {
        private readonly IMemberRepository _repo;
        private readonly INotificationRepository _notificationRepo;

        public MemberController(IMemberRepository repo, INotificationRepository notificationRepo)
        {
            _repo = repo;
            _notificationRepo = notificationRepo;
        }

        // 🔹 Show all members
        public async Task<IActionResult> Index()
        {
            var members = await _repo.GetAllAsync();
            var dtos = members.Select(p => new MemberDto
            {
                MemberId = p.MemberId,
                MemberName = p.MemberName,
                Address = p.Address,
                MemberType = p.MemberType,
                PhoneNumber = p.PhoneNumber,
                Email = p.Email,
                JoinDate = p.JoinDate,
                ExpiryDate = p.ExpiryDate,
                Status = p.Status
            });

            return View(dtos);
        }

        // 🔹 Create GET
        [HttpGet]
        public IActionResult Create() => View();

        // 🔹 Create POST
        [HttpPost]
        public async Task<IActionResult> Create(MemberDto dto)
        {
            if (ModelState.IsValid)
            {
                var member = new Member
                {
                    MemberId = Guid.NewGuid(),
                    MemberName = dto.MemberName,
                    Address = dto.Address,
                    MemberType = dto.MemberType,
                    PhoneNumber = dto.PhoneNumber,
                    Email = dto.Email,
                    JoinDate = dto.JoinDate,
                    ExpiryDate = dto.ExpiryDate,
                    Status = dto.Status
                };

                await _repo.AddAsync(member);

                // ✅ Add notification
                var notification = new NotificationDto
                {
                    Title = "New Member Added",
                    Message = $"Member '{dto.MemberName}' has been added to the library.",
                    Category = "Member",
                    IsRead = false,
                    CreatedAt = DateTime.UtcNow
                };
                await _notificationRepo.AddAsync(notification);

                return RedirectToAction(nameof(Index));
            }
            return View(dto);
        }

        // 🔹 Edit GET
        [HttpGet]
        public async Task<IActionResult> Edit(Guid id)
        {
            var member = await _repo.GetByIdAsync(id);
            if (member == null)
                return NotFound();

            var dto = new MemberDto
            {
                MemberId = member.MemberId,
                MemberName = member.MemberName,
                Address = member.Address,
                MemberType = member.MemberType,
                PhoneNumber = member.PhoneNumber,
                Email = member.Email,
                JoinDate = member.JoinDate,
                ExpiryDate = member.ExpiryDate,
                Status = member.Status
            };

            return View(dto);
        }

        // 🔹 Edit POST
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(MemberDto dto)
        {
            if (!ModelState.IsValid)
                return View(dto);

            var memberToUpdate = await _repo.GetByIdAsync(dto.MemberId);
            if (memberToUpdate == null)
                return NotFound();

            memberToUpdate.MemberName = dto.MemberName;
            memberToUpdate.Address = dto.Address;
            memberToUpdate.MemberType = dto.MemberType;
            memberToUpdate.PhoneNumber = dto.PhoneNumber;
            memberToUpdate.Email = dto.Email;
            memberToUpdate.JoinDate = dto.JoinDate;
            memberToUpdate.ExpiryDate = dto.ExpiryDate;
            memberToUpdate.Status = dto.Status;

            await _repo.UpdateAsync(memberToUpdate);

            // ✅ Add notification
            var notification = new NotificationDto
            {
                Title = "Member Updated",
                Message = $"Member '{dto.MemberName}' information has been updated.",
                Category = "Member",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await _notificationRepo.AddAsync(notification);

            return RedirectToAction(nameof(Index));
        }

        // 🔹 Delete GET
        [HttpGet]
        public async Task<IActionResult> Delete(Guid id)
        {
            var member = await _repo.GetByIdAsync(id);
            if (member == null) return NotFound();

            var dto = new MemberDto
            {
                MemberId = member.MemberId,
                MemberName = member.MemberName,
                Address = member.Address,
                MemberType = member.MemberType,
                PhoneNumber = member.PhoneNumber,
                Email = member.Email,
                JoinDate = member.JoinDate,
                ExpiryDate = member.ExpiryDate,
                Status = member.Status
            };

            return View(dto);
        }

        // 🔹 Delete POST
        [HttpPost]
        public async Task<IActionResult> Delete(MemberDto dto)
        {
            if (dto.MemberId == default)
                return BadRequest();

            await _repo.DeleteAsync(dto.MemberId);

            // ✅ Add notification
            var notification = new NotificationDto
            {
                Title = "Member Deleted",
                Message = $"Member '{dto.MemberName}' has been removed from the library system.",
                Category = "Member",
                IsRead = false,
                CreatedAt = DateTime.UtcNow
            };
            await _notificationRepo.AddAsync(notification);

            return RedirectToAction(nameof(Index));
        }
    }
}
