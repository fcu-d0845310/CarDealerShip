using CarDealerShip.Validation;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CarDealerShip.ViewModels
{
    public class CarCreateViewModel
    {
        public string? ReturnUrl { get; set; }//存檔或取消後要回去的列表網址

        [Required(ErrorMessage = "請選擇分店")]
        [Display(Name = "分店")]
        public int? BranchId { get; set; }

        [Required(ErrorMessage = "請輸入品牌")]
        [MaxLength(50, ErrorMessage = "品牌不可超過 50 個字")]
        [Display(Name = "品牌")]
        public string Brand { get; set; } = null!;

        [Required(ErrorMessage = "請輸入型號")]
        [MaxLength(50, ErrorMessage = "型號不可超過 50 個字")]
        [Display(Name = "型號")]
        public string Model { get; set; } = null!;

        [Required(ErrorMessage = "請輸入顏色")]
        [MaxLength(20, ErrorMessage = "顏色不可超過 20 個字")]
        [Display(Name = "顏色")]
        public string Color { get; set; } = null!;

        [Range(1900, 2100, ErrorMessage = "年份必須介於 1900 到 2100")]
        [Display(Name = "年份")]
        public int ProductionYear { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "里程不可小於 0")]
        [Display(Name = "里程")]
        public int Mileage { get; set; }

        [Range(0, int.MaxValue, ErrorMessage = "排氣量不可小於 0")]
        [Display(Name = "排氣量")]
        public int Displacement { get; set; }

        [Range(typeof(decimal), "0", "99999999.99", ErrorMessage = "成本必須介於 0 到 99,999,999.99")]
        [Display(Name = "成本")]
        public decimal CostPrice { get; set; }

        [Range(typeof(decimal), "0", "99999999.99", ErrorMessage = "售價必須介於 0 到 99,999,999.99")]
        [Display(Name = "售價")]
        public decimal SellingPrice { get; set; }

        [Display(Name = "描述")]
        public string? Description { get; set; }
        [MinLength(1, ErrorMessage = "至少1張圖片")]
        [MaxLength(10, ErrorMessage = "至多10張圖片")]
        [AllowedExtensions(".jpg", ".png", ".jpeg", ErrorMessage = "只能上傳 \".jpg\"、 \".png\"、 \".jpeg\"")]
        [MaxFileSize(5 * 1024 * 1024, ErrorMessage = "檔案最大5MB")]
        public List<IFormFile> Images { get; set; } = new();
        public List<SelectListItem> Branches { get; set; } = new();//分店下拉選單
    }
}
