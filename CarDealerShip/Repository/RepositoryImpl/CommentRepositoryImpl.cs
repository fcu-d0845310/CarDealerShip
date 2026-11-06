using CarDealerShip.Data;
using CarDealerShip.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealerShip.Repository.RepositoryImpl
{
    public class CommentRepositoryImpl : ICommentRepository
    {
        private readonly AppDbContext _appDbContext;

        public CommentRepositoryImpl(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task DeleteCommentAsync(List<Comment> toDelete)
        {
            _appDbContext.Comments.RemoveRange(toDelete);
            await _appDbContext.SaveChangesAsync();
        }

        public async Task<Comment?> GetCommentByIdAsync(int id)
        {
            return await _appDbContext.Comments
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
           
        }

        public async Task<List<Comment>> GetCommentByPostAsync(int postId)
        {
           return await _appDbContext.Comments
                .Where(c => c.PostId == postId)
                .ToListAsync();
        }


    }
}
