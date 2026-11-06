namespace CarDealerShip.Dtos.Post
{
    //一則留言，回覆以樹狀結構放在 Replies
    public class CommentDto
    {
        public int Id { get; set; }
        public string Content { get; set; } = null!;
        public string UserName { get; set; } = null!;
        public DateTime CreatedAt { get; set; }
        public int? ParentId { get; set; }//父留言 Id，最上層留言為 null
        public List<CommentDto> Replies { get; set; } = new();
    }
}
