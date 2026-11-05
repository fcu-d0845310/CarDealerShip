namespace CarDealerShip.ViewModels
{
    public class PostListItemViewModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Category {  get; set; }
        public string UserName {  get; set; }
        public string CarName {  get; set; }
        public int CommentCount {  get; set; }
        public DateTime CreatedAt { get; set; }
    }
}
