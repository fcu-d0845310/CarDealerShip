using CarDealerShip.Data;
using CarDealerShip.Models;
using Microsoft.EntityFrameworkCore;



namespace CarDealerShip.Repository.RepositoryImpl
{
    public class PostRepositoryImpl : IPostRepository
    {
        private readonly AppDbContext _appDbContext;

        public PostRepositoryImpl(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task DeletePostByIdAsync(int id)
        {
            var post = await _appDbContext.Posts.FindAsync(id);

            var comments = await _appDbContext.Comments
                .Where(c => c.PostId == id)
                .ToListAsync();

            _appDbContext.Comments.RemoveRange(comments);
            _appDbContext.Posts.Remove(post);

            await _appDbContext.SaveChangesAsync();
        }

        //計算符合的貼文數
        public async Task<int> CountPostsAsync(string? keyword, string? category)
        {
            return await BuildPostQuery(keyword, category).CountAsync();
        }

        //查詢該page的貼文
        public async Task<List<Post>> GetPostAsync(string? keyword, string? category, int skip, int take)
        {
            return await BuildPostQuery(keyword, category)
                .Include(p => p.User)
                .Include(p => p.Car)
                .OrderByDescending(p => p.CreatedAt)
                .ThenByDescending(p => p.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }

        //一次查出當頁所有貼文的留言數
        public async Task<Dictionary<int, int>> GetCommentCountsAsync(List<int> postIds)
        {
            return await _appDbContext.Comments.AsNoTracking()
                .Where(c => postIds.Contains(c.PostId))
                .GroupBy(c => c.PostId)
                .Select(g => new { PostId = g.Key, Count = g.Count() })
                .ToDictionaryAsync(x => x.PostId, x => x.Count);
        }

        public async Task<List<Comment>> GetCommentByPostIdAsync(int id)
        {
            return await _appDbContext.Comments.AsNoTracking()
                 .Include(c => c.User)
                 .Where(c => c.PostId == id)
                 .OrderBy(c => c.CreatedAt)
                 .ToListAsync();
        }

        public async Task<Post?> GetPostByIdAsync(int id)
        {
            return await _appDbContext.Posts.AsNoTracking()
                .Include(p => p.User)
                .Include(p => p.Car)
                .FirstOrDefaultAsync(p => p.Id == id);
        }

        //篩選條件統一處理
        private IQueryable<Post> BuildPostQuery(string? keyword, string? category)
        {
            var query = _appDbContext.Posts.AsNoTracking();

            //兩個if判斷篩選和搜尋
            if (!string.IsNullOrEmpty(category))
            {
                query = query.Where(p => p.Category == category);
            }

            if (!string.IsNullOrEmpty(keyword))
            {
                var Keyword = keyword.Trim();//去除空格
                query = query.Where(p => p.Title.Contains(Keyword) || (p.Car != null && (p.Car.Brand.Contains(Keyword) || p.Car.Model.Contains(Keyword))));
            }

            return query;
        }
    }
}
