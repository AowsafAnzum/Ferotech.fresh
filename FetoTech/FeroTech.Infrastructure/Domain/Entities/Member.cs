using System.ComponentModel.DataAnnotations;

namespace FeroTech.Infrastructure.Domain.Entities
{
   public class Member
    {
        [Key]
        public Guid MemberId { get; set; }
        public string? MemberName { get; set; }
        public string? MemberType { get; set; } // Student, Staff, External
        public string? Address { get; set; }
        public string? PhoneNumber { get; set; }
        public string? Email { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime ExpiryDate { get; set; }
        public string? Status { get; set; } // Active, Inactive
      
    }
}
