using System;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class GroupDealUser
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int DealId { get; set; }
        public GroupDeal GroupDeal { get; set; }

        [Required]
        public string UserId { get; set; }
        public ApplicationUser User { get; set; }

        public DateTime JoinedDate { get; set; } = DateTime.UtcNow;
    }
}
