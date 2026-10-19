using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("Reservation")]
    public class Reservation
    {
        [Key]
        public int Id { get; set; }

        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public DateTime ReservationDate { get; set; }

        [Required, MaxLength(20)]
        public string Status { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
