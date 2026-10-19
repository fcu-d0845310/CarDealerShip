using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("Car")]
    public class Car
    {
        [Key]
        public int Id { get; set; }

        public int BranchId { get; set; }
        [ForeignKey(nameof(BranchId))]
        public Branch Branch { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Brand { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Model { get; set; } = null!;

        public int ProductionYear { get; set; }
        public int Mileage { get; set; }
        public int Displacement { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal SellingPrice { get; set; }

        [Column(TypeName = "decimal(10, 2)")]
        public decimal CostPrice { get; set; }

        [Required, MaxLength(20)]
        public string Status { get; set; } = null!;

        public string? Description { get; set; }
        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<CarImage> CarImages { get; set; } = new List<CarImage>();
    }
}
