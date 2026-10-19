using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("Branch")]
    public class Branch
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        [Required, MaxLength(100)]
        public string Address { get; set; } = null!;

        [Required, MaxLength(9)]
        public string Phone { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Car> Cars { get; set; } = new List<Car>();
    }
}
