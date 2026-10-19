using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("SalesRecord")]
    public class SalesRecord
    {
        [Key]
        public int Id { get; set; }

        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Column(TypeName = "decimal(10, 2)")]
        public decimal FinalPrice { get; set; }

        public DateTime SaleDate { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
