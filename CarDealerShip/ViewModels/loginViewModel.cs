using System.ComponentModel.DataAnnotations;

namespace CarDealerShip.ViewModels
{
    public class loginViewModel
    {
        [Required(ErrorMessage = "請輸入 Email")]
        [EmailAddress(ErrorMessage = "Email 格式不正確")]
        [MaxLength(255)]
        [Display(Name = "信箱")]
        public string Email { get; set; } = null!;

        [Required(ErrorMessage = "請輸入密碼")]
        [DataType(DataType.Password)]
        [Display(Name = "密碼")]
        public string Password { get; set; } = null!;
    }
}
