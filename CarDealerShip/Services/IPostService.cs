using CarDealerShip.ViewModels;

namespace CarDealerShip.Services
{
    public interface IPostService
    {
        Task<bool> DeletePostAsync(int id);
        Task<PostListViewModel> GetAllPostAsync(PostListViewModel vm);
        Task<PostDetailViewModel?> GetPostDetailAsync(int id);
    }
}
