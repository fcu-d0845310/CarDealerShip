using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("TradeInRequest")]
    public class TradeInRequest
    {
        [Key]
        public int Id { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Brand { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Model { get; set; } = null!;

        public int ProductionYear { get; set; }
        public int Mileage { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal ExpectedPrice { get; set; }

        [Required, MaxLength(20)]
        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<TradeImage> TradeImages { get; set; } = new List<TradeImage>();
    }
}
