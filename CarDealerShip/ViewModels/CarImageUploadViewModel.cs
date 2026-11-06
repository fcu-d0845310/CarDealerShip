using CarDealerShip.Validation;
using System.ComponentModel.DataAnnotations;

namespace CarDealerShip.ViewModels
{
    public class CarImageUploadViewModel
    {
        public string? ReturnUrl {  get; set; }

        [MinLength(1, ErrorMessage = "至少1張圖片")]
        [MaxLength(10, ErrorMessage = "至多10張圖片")]
        [AllowedExtensions(".jpg", ".png", ".jpeg", ErrorMessage = "只能上傳 \".jpg\"、 \".png\"、 \".jpeg\"")]
        [MaxFileSize(5 * 1024 * 1024,ErrorMessage ="檔案最大5MB")]
        public List<IFormFile> Files { get; set; } = new();
    }
}
