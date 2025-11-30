using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Models
{
    public class GroupDeal
    {
        [Key]
        public int DealId { get; set; }

        [Required]
        public int ProductId { get; set; }

        [Required]
        public int RequiredUsers { get; set; }

        public int JoinedUsersCount { get; set; } = 0;

        [Required]
        [Column(TypeName = "decimal(18,2)")]
        public decimal DiscountPrice { get; set; }

        public DateTime ExpirationDate { get; set; }

        public bool IsCompleted { get; set; } = false;

        public virtual Product Product { get; set; }
        public virtual ICollection<GroupDealUser> GroupDealUsers { get; set; }
    }
}
