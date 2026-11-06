namespace CarDealerShip.Dtos.Post
{
    //單一貼文的完整資料
    public class PostDetailDto
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Category { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; } = null!;
        public int? CarId { get; set; }//關聯車輛 Id，沒有關聯時為 null
        public string? CarName { get; set; }
        public List<CommentDto> Comments { get; set; } = new();//只放最上層留言，回覆在各留言的 Replies
    }
}
