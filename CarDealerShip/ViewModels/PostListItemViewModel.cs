namespace CarDealerShip.ViewModels
{
    public class PostListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; } = null!;
        public string Category {  get; set; } = null!;
        public string UserName {  get; set; } = null!;
        public string? CarName {  get; set; }//沒有關聯車輛時為 null
        public int CommentCount {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
