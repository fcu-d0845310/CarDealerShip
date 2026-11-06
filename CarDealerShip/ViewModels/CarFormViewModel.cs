using CarDealerShip.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CarDealerShip.ViewModels
{
    public class CarFormViewModel
    {
        public int? Id { get; set; }//新增時為 null，編輯時有值
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

        [Required(ErrorMessage = "請選擇狀態")]
        [AllowedValues(CarStatus.InStock, CarStatus.Reserved, CarStatus.Sold, CarStatus.Removed, ErrorMessage = "狀態不存在")]
        [Display(Name = "狀態")]
        public string Status { get; set; } = null!;

        [Display(Name = "描述")]
        public string? Description { get; set; }

        public List<IFormFile> Images { get; set; } = new();//新上傳的圖片
        public List<int> DeleteImageIds { get; set; } = new();//編輯時要刪除的圖片
        public int? PrimaryImageId { get; set; }//指定為主圖的現有圖片

        public List<CarImageViewModel> ExistingImages { get; set; } = new();//顯示用，由 Service 查詢
        public List<SelectListItem> Branches { get; set; } = new();//分店下拉選單，由 Service 查詢
    }
}
