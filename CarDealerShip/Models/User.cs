using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("User")]
    public class User
    {
        [Key]
        public int Id { get; set; }

        [Required, MaxLength(255)]
        public string Email { get; set; } = null!;

        [MaxLength(60)]
        public string? Password { get; set; }

        [Required, MaxLength(50)]
        public string Name { get; set; } = null!;

        [MaxLength(10)]
        public string? Phone { get; set; }

        [MaxLength(10)]
        public string? IdentityNumber { get; set; }

        [MaxLength(100)]
        public string? Address { get; set; }

        public int RoleId { get; set; }
        [ForeignKey(nameof(RoleId))]
        public Role Role { get; set; } = null!;

        [MaxLength(50)]
        public string? AuthProvider { get; set; }

        public string? ProviderId { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime? LastLoginAt { get; set; }
    }
}
