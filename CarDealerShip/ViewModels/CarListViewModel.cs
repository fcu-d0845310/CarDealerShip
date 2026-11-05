using CarDealerShip.Constants;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace CarDealerShip.ViewModels
{
    public class CarListViewModel
    {
        
        public string? Brand {  get; set; }//品牌篩選，完全比對
        public string? Model {  get; set; }//型號篩選，部分比對
        public List<SelectListItem> Brands { get; set; } = new();//品牌下拉選單的選項
        [AllowedValues(null,CarStatus.InStock, CarStatus.Reserved, CarStatus.Removed, CarStatus.Sold)]
        public string? Status {  get; set; }
        public int? BranchId {  get; set; }
        public List<SelectListItem> Branches { get; set; } = new();//下拉選單
        private int _page = 1;
        [Range(1, 100000, ErrorMessage = "頁碼超出範圍")]
        public int Page
        {
            get => _page;
            set => _page = value < 1 ? 1 : value;
        }
        public int PageSize { get; } = 12;
        public int TotalCount{  get; set; }
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);//計算多少頁數
        public List<CarListItemViewModel> Items { get; set; } = new();



    }
}
