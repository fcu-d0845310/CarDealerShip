using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("CarImage")]
    public class CarImage
    {
        [Key]
        public int Id { get; set; }

        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

        [Required, MaxLength(255)]
        public string ImageUrl { get; set; } = null!;

        public bool IsPrimary { get; set; }

        [MaxLength(20)]
        public string? ImageType { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
