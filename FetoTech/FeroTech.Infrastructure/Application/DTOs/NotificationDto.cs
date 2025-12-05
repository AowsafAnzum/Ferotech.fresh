using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeroTech.Infrastructure.Application.DTOs
{
    public class NotificationDto
    {
        public Guid NotificationId { get; set; } = Guid.NewGuid();
        public Guid? UserId { get; set; }          // optional: target a particular user
        public string Title { get; set; } = string.Empty;
        public string Message { get; set; } = string.Empty;
        public string Category { get; set; } = "General";
        public bool IsRead { get; set; } = false;
        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;
    }
}
