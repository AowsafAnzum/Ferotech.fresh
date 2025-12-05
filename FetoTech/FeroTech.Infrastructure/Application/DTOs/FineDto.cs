using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FeroTech.Infrastructure.Application.DTOs
{
    public class FineDto
    {
      
        public Guid FineId { get; set; }
        public Guid MemberId { get; set; }
        public Guid IssueId { get; set; }
        public decimal FineAmount { get; set; }
        public DateTime FineDate { get; set; }
        public string? PaymentStatus { get; set; } // Paid / Unpaid
    }
}
