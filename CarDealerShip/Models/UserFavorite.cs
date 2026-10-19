using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace CarDealerShip.Models
{
    [Table("UserFavorite")]
    [PrimaryKey(nameof(UserId), nameof(CarId))] // 複合主鍵
    public class UserFavorite
    {
        public int UserId { get; set; }
        [ForeignKey(nameof(UserId))]
        public User User { get; set; } = null!;

        public int CarId { get; set; }
        [ForeignKey(nameof(CarId))]
        public Car Car { get; set; } = null!;

        public DateTime CreatedAt { get; set; } = DateTime.Now;
    }
}
