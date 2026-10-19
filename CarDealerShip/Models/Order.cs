using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("Order")]
    public class Order
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string OrderNo { get; set; } = null!;

        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal DepositAmount { get; set; }

        [Required, MaxLength(20)]
        public string PaymentStatus { get; set; } = null!;

        [MaxLength(50)]
        public string? PaymentMethod { get; set; }

        [MaxLength(100)]
        public string? TradeNo { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? PaidAt { get; set; }
    }
}
