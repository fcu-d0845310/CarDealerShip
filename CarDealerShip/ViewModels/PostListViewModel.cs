using System.ComponentModel.DataAnnotations;
using CarDealerShip.Constants;

namespace CarDealerShip.ViewModels
{
    public class PostListViewModel
    {
        public List<PostListItemViewModel> Items { get; set; } = new();
        [MaxLength(50, ErrorMessage = "搜尋關鍵字不可超過 50 個字")]
        public string? Keyword { get; set; }//搜尋
        [AllowedValues(null, PostCategory.Review, PostCategory.Insurance,
            PostCategory.Loan, PostCategory.Maintenance, ErrorMessage = "分類不存在")]
        public string? Category { get; set; }//分類篩選

        private int _page = 1;
        [Range(1, 100000, ErrorMessage = "頁碼超出範圍")]
        public int Page
        {
            get => _page;
            set => _page = value < 1 ? 1 : value;
        }
        public int PageSize { get; } = 10;//一頁多少貼文，固定不修改
        public int TotalCount { get; set; }//符合條件的總筆數
        public int TotalPages => (int)Math.Ceiling(TotalCount / (double)PageSize);//計算多少頁數
    }
}
