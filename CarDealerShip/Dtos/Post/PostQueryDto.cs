namespace CarDealerShip.Dtos.Post
{
    //貼文列表的查詢條件
    public class PostQueryDto
    {
        public string? Keyword { get; set; }//搜尋標題或車輛品牌、型號
        public string? Category { get; set; }//分類
        public int Page { get; set; } = 1;//要查詢的頁碼
    }
}
