using FeroTech.Infrastructure.Application.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeroTech.Infrastructure.Application.Interfaces
{
    public interface INotificationRepository
    {
        Task<IEnumerable<NotificationDto>> GetAllAsync();
        Task<NotificationDto?> GetByIdAsync(Guid id);
        Task AddAsync(NotificationDto notification);
        Task UpdateAsync(NotificationDto notification);
        Task DeleteAsync(Guid id);
    }
}


