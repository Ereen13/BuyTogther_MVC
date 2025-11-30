using Microsoft.AspNetCore.Identity;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Models
{
    public class ApplicationUser : IdentityUser
    {
        
        public string Name { get; set; }
        public string? StreetAddress { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public string? PostalCode { get; set; }
        public int? CompanyId { get; set; }
        public Company? Company { get; set; }
        public string? ProfilePictureUrl { get; set; } // << ده اللي ضفناه عشان الصورة
        [NotMapped]
        public string? Role { get; set; }
    }
}

