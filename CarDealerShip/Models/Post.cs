using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("Post")]
    public class Post
    {
        [Key]
        public int Id { get; set; }

        public int? CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car? Car { get; set; }

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required, MaxLength(50)]
        public string Title { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        [Required, MaxLength(20)]
        public string Category { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;

        public ICollection<Comment> Comments { get; set; } = new List<Comment>();
    }
}
