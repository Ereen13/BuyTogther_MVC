using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace BulkyBook.Models
{
    public class GroupDeal
    {
        [Key]
        public int DealId { get; set; }

        [Required]
        public int ProductId { get; set; }
        public Product Product { get; set; }

        [Required]
        public int RequiredUsers { get; set; }

        public int JoinedUsersCount => GroupDealUsers?.Count ?? 0;


        [Required]
        public decimal GroupPrice { get; set; }

        public decimal OriginalPrice { get; set; }


        public DateTime EndDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public bool IsActive { get; set; } = true;


        [Required]
        public DateTime StartDate { get; set; } = DateTime.UtcNow;

        public ICollection<GroupDealUser> GroupDealUsers { get; set; }
    }
}
