using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("Comment")]
    public class Comment
    {
        [Key]
        public int Id { get; set; }

        public int PostId { get; set; }
        [ForeignKey(nameof(PostId))]
        public Post Post { get; set; } = null!;

        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        [Required]
        public string Content { get; set; } = null!;

        public int? ParentId { get; set; }
        [ForeignKey(nameof(ParentId))]
        public Comment? ParentComment { get; set; }

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
