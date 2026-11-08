using System.ComponentModel.DataAnnotations;

namespace CarDealerShip.ViewModels
{
    //新增與編輯分店共用的表單
    public class BranchFormViewModel
    {
        public int? Id { get; set; }//新增時為 null，編輯時有值

        [Required(ErrorMessage = "請輸入分店名稱")]
        [MaxLength(50, ErrorMessage = "分店名稱不可超過 50 個字")]
        [Display(Name = "分店名稱")]
        public string Name { get; set; } = null!;

        [Required(ErrorMessage = "請輸入地址")]
        [MaxLength(100, ErrorMessage = "地址不可超過 100 個字")]
        [Display(Name = "地址")]
        public string Address { get; set; } = null!;

        [Required(ErrorMessage = "請輸入電話")]
        [MaxLength(9, ErrorMessage = "電話不可超過 9 個字")]
        [RegularExpression(@"^\d+$", ErrorMessage = "電話只能輸入數字")]
        [Display(Name = "電話")]
        public string Phone { get; set; } = null!;
    }
}
