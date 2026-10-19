using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("TradeImage")]
    public class TradeImage
    {
        [Key]
        public int Id { get; set; }

        public int TradeInRequestId { get; set; }
        [ForeignKey(nameof(TradeInRequestId))]
        public TradeInRequest TradeInRequest { get; set; } = null!;

        [Required, MaxLength(255)]
        public string ImageUrl { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
