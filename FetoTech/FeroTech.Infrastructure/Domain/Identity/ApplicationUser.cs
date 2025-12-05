using Microsoft.AspNetCore.Identity;
using System;

namespace FeroTech.Infrastructure.Domain.Identity
{
    public class ApplicationUser : IdentityUser
    {
        // add extra profile fields if needed
        public string? FullName { get; set; }
        public string PhoneNumber { get; set; }
    }
}
