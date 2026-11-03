namespace CarDealerShip.ViewModels
{
    public class PostDetailViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Content { get; set; } = null!;
        public string Category { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public string AuthorName { get; set; } = null!;
        public int? CarId { get; set; }// 用來做「查看車輛」的連結
        public string? CarName { get; set; }
        // 只放root留言，回覆放在每則留言的 Replies 裡
        public List<CommentViewModel> Comments { get; set; } = new();
    }
}
