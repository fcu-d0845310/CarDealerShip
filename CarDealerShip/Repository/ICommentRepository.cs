using CarDealerShip.Models;

namespace CarDealerShip.Repository
{
    public interface ICommentRepository
    {
        Task<Comment> GetCommentByIdAsync(int id);
        Task<List<Comment>> GetCommentByPostAsync(int postId);

        Task DeleteCommentAsync(List<Comment> toDelete);
    }
}
