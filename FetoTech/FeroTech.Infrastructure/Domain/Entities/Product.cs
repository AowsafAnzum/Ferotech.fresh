using System.ComponentModel.DataAnnotations;

namespace FeroTech.Infrastructure.Domain.Entities
{
    public class Product
    {
        [Key]
        public int MemberId { get; set; }
        public string? Name { get; set; }
        public decimal Price { get; set; }
       
    }
}
