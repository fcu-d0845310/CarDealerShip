using CarDealerShip.Dtos.Common;
using CarDealerShip.Dtos.Post;

namespace CarDealerShip.Services
{
    public interface IPostService
    {
        Task<bool> DeletePostAsync(int id);
        Task<PagedResult<PostListItemDto>> GetAllPostAsync(PostQueryDto query);
        Task<PostDetailDto?> GetPostDetailAsync(int id);
    }
}
