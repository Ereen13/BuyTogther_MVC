using BulkyBook.Models;


using System.ComponentModel.DataAnnotations;

public class GroupDealUser
{
    [Key]
    public int Id { get; set; }

    [Required]
    public int DealId { get; set; }

    [Required]
    public string UserId { get; set; }

    public DateTime JoinedDate { get; set; } = DateTime.Now;

    public virtual GroupDeal GroupDeal { get; set; }
    public virtual ApplicationUser User { get; set; }   // <-- الأفضل تضيفه
}
