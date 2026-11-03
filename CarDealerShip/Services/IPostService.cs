using CarDealerShip.ViewModels;

namespace CarDealerShip.Services
{
    public interface IPostService
    {
        Task<PostListViewModel> GetAllPost(PostListViewModel vm);
        Task<PostDetailViewModel> GetPostDetailAsync(int id);
    }
}
