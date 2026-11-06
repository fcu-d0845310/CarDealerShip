namespace CarDealerShip.Dtos.Post
{
    //貼文列表的一筆資料
    public class PostListItemDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Category { get; set; } = null!;
        public string UserName { get; set; } = null!;//發文者名稱
        public string? CarName { get; set; }//關聯車輛的品牌與型號，沒有關聯車輛時為 null
        public int CommentCount { get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
