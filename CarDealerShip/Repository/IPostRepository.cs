using CarDealerShip.Models;

namespace CarDealerShip.Repository
{
    public interface IPostRepository
    {
        Task DeletePostByIdAsync(int id);
        Task<int> CountPostsAsync(string? keyword, string? category);
        Task<List<Post>> GetPostAsync(string? keyword, string? category, int skip, int take);
        Task<Dictionary<int, int>> GetCommentCountsAsync(List<int> postIds);
        Task<List<Comment>> GetCommentByPostIdAsync(int id);
        Task<Post?> GetPostByIdAsync(int id);
    }
}
