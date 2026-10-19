using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("Role")]
    public class Role
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        public ICollection<User> Users { get; set; } = new List<User>();
    }
}
