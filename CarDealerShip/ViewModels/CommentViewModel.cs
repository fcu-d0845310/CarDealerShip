namespace CarDealerShip.ViewModels
{
    public class CommentViewModel
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }

        public List<CommentViewModel> Replies { get; set; } = new();
    }
}
