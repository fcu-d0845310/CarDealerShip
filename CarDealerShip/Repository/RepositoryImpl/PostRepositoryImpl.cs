using CarDealerShip.Data;
using CarDealerShip.ViewModels;
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
        public async Task<PostListViewModel> GetAllPost(PostListViewModel vm)
        {
            var query = _appDbContext.Posts.AsNoTracking();

            //兩個if判斷篩選和搜尋
            if (!string.IsNullOrEmpty(vm.Category))
            {
                query = query.Where(p => p.Category == vm.Category);
            }

            if (!string.IsNullOrEmpty(vm.Keyword))
            {
                var keyword = vm.Keyword.Trim();
                query = query.Where(p => p.Title.Contains(keyword) || (p.Car != null && (p.Car.Brand.Contains(keyword) || p.Car.Model.Contains(keyword))));
            }
            var count = await query.CountAsync();

            var totalPages = (int)Math.Ceiling(count / (double)vm.PageSize);
            var page = vm.Page;

            if (page > totalPages && totalPages > 0)
            {
                page = totalPages;//超過就顯示最後一頁
            }


            var items = await query.OrderByDescending(p => p.CreatedAt)
                .ThenByDescending(p => p.Id)
                .Skip((page - 1) * vm.PageSize)
                .Take(vm.PageSize)
                .Select(p => new PostListItemViewModel 
                {
                    Id = p.Id,
                    Title = p.Title,
                    Category = p.Category,
                    Content = p.Content,
                    UserName = p.User.Name,
                    CarName = p.Car != null ? p.Car.Brand + " " + p.Car.Model : null,
                    CommentCount = p.Comments.Count(),
                    CreatedAt = p.CreatedAt
                })
                .ToListAsync();


            return new PostListViewModel
            {
                Items = items,
                Keyword = vm.Keyword,
                Category = vm.Category,
                Page = page,
                TotalCount = count
            };

           
        }

        public async Task<List<CommentViewModel>> GetCommentByPostIdAsync(int id)
        {
            var comments = await _appDbContext.Comments.AsNoTracking()
                 .Where(c => c.PostId == id)
                 .OrderBy(c => c.CreatedAt)
                 .Select(c => new CommentViewModel
                 {
                     Id = c.Id,
                     Content = c.Content,
                     UserName = c.User.Name,
                     CreatedAt = c.CreatedAt,
                     ParentId = c.ParentId,
                 }).ToListAsync();
            return comments;
        }

        public async Task<PostDetailViewModel?> GetPostById(int id)
        {    
            var post = await _appDbContext.Posts.AsNoTracking()
                .Where(p => p.Id == id)
                .Select(p => new PostDetailViewModel
                {
                    Id = p.Id,
                    Title = p.Title,
                    Content = p.Content,
                    Category = p.Category,
                    CreatedAt = p.CreatedAt,
                    AuthorName = p.User.Name,
                    CarId = p.CarId,
                    CarName = p.Car != null ? p.Car.Brand + " " + p.Car.Model:null
                })
                .FirstOrDefaultAsync();
            return post;
        }
    }
}
