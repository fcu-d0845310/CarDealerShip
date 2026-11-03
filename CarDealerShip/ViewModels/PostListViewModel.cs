using System.ComponentModel.DataAnnotations;

namespace CarDealerShip.ViewModels
{
    public class PostListViewModel
    {
        public List<PostListItemViewModel> Items { get; set; } = new();
        [MaxLength(50)]
        public string? Keyword { get; set; }//搜尋
        [MaxLength(20)]
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
