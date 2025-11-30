using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BulkyBook.Models
{
    public class Payment
    {
        [Key]
        public int Id { get; set; }

        public string TransactionId { get; set; } // special reference / merchant_order_id

        [Column(TypeName = "decimal(18,2)")]
        public decimal Amount { get; set; }

        public DateTime PaymentDate { get; set; } = DateTime.UtcNow;

        public string PaymentMethod { get; set; } // card / wallet

        public string Status { get; set; } = "Pending"; // Pending / Success / Failed

        // Relations (optional depending on your domain)
        public int? OrderHeaderId { get; set; }
        public OrderHeader OrderHeader { get; set; }

        public string ApplicationUserId { get; set; }
        public ApplicationUser ApplicationUser { get; set; }
    }
}

