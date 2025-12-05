using FeroTech.Infrastructure.Application.DTOs;
using FeroTech.Infrastructure.Application.Interfaces;
using FeroTech.Infrastructure.Data;
using FeroTech.Infrastructure.Domain.Entities;
using Microsoft.EntityFrameworkCore;

namespace FeroTech.Infrastructure.Repositories
{
    public class NotificationRepository : INotificationRepository
    {
        private readonly ApplicationDbContext _context;

        public NotificationRepository(ApplicationDbContext context)
        {
            _context = context;
        }

        // Get all notifications
        public async Task<IEnumerable<NotificationDto>> GetAllAsync()
        {
            return await _context.Notifications
                                .Select(n => new NotificationDto
                                {
                                    NotificationId = n.NotificationId,
                                    UserId = n.UserId,
                                    Title = n.Title,
                                    Message = n.Message,
                                    Category = n.Category,
                                    IsRead = n.IsRead,
                                    CreatedAt = n.CreatedAt
                                })
                                .ToListAsync();
        }

        // Get single notification by ID
        public async Task<NotificationDto?> GetByIdAsync(Guid id)
        {
            var n = await _context.Notifications.FindAsync(id);

            if (n == null) return null;

            return new NotificationDto
            {
                NotificationId = n.NotificationId,
                UserId = n.UserId,
                Title = n.Title,
                Message = n.Message,
                Category = n.Category,
                IsRead = n.IsRead,
                CreatedAt = n.CreatedAt
            };
        }

        // Add new notification
        public async Task AddAsync(NotificationDto dto)
        {
            var notification = new Notification
            {
                NotificationId = dto.NotificationId == Guid.Empty ? Guid.NewGuid() : dto.NotificationId,
                UserId = dto.UserId,
                Title = dto.Title,
                Message = dto.Message,
                Category = dto.Category ?? "General",
                IsRead = dto.IsRead,
                CreatedAt = dto.CreatedAt == default ? DateTime.UtcNow : dto.CreatedAt
            };

            await _context.Notifications.AddAsync(notification);
            await _context.SaveChangesAsync();
        }

        // Update notification (for example, mark as read)
        public async Task UpdateAsync(NotificationDto dto)
        {
            var existing = await _context.Notifications.FindAsync(dto.NotificationId);
            if (existing == null) return;

            existing.UserId = dto.UserId;
            existing.Title = dto.Title;
            existing.Message = dto.Message;
            existing.Category = dto.Category ?? existing.Category;
            existing.IsRead = dto.IsRead;
            existing.CreatedAt = dto.CreatedAt == default ? existing.CreatedAt : dto.CreatedAt;

            _context.Notifications.Update(existing);
            await _context.SaveChangesAsync();
        }

        // Delete a notification
        public async Task DeleteAsync(Guid id)
        {
            var notification = await _context.Notifications.FindAsync(id);
            if (notification != null)
            {
                _context.Notifications.Remove(notification);
                await _context.SaveChangesAsync();
            }
        }
    }

}
