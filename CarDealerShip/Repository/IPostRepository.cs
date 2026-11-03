using CarDealerShip.ViewModels;

namespace CarDealerShip.Repository
{
    public interface IPostRepository
    {
        Task<PostListViewModel> GetAllPost(PostListViewModel vm);
        Task<List<CommentViewModel>> GetCommentByPostIdAsync(int id);
        Task<PostDetailViewModel?> GetPostById(int id);
    }
}
