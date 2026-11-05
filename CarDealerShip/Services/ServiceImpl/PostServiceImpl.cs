using CarDealerShip.Repository;
using CarDealerShip.ViewModels;

namespace CarDealerShip.Services.ServiceImpl
{
    public class PostServiceImpl : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostServiceImpl(IPostRepository repo)
        {
            _postRepository = repo;
        }

        public async Task<PostDetailViewModel?> GetPostDetailAsync(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return null;
            }

            var vm = new PostDetailViewModel
            {
                Id = post.Id,
                Title = post.Title,
                Content = post.Content,
                Category = post.Category,
                CreatedAt = post.CreatedAt,
                AuthorName = post.User.Name,
                CarId = post.CarId,
                CarName = post.Car != null ? post.Car.Brand + " " + post.Car.Model : null
            };

            var commentEntities = await _postRepository.GetCommentByPostIdAsync(id);
            var Comments = commentEntities.Select(c => new CommentViewModel
            {
                Id = c.Id,
                Content = c.Content,
                UserName = c.User.Name,
                CreatedAt = c.CreatedAt,
                ParentId = c.ParentId
            }).ToList();
            var LookUp = Comments.ToLookup(c => c.ParentId);//根據parentId轉換成字典型態
            foreach (var c in Comments)
            {
                c.Replies = LookUp[c.Id].ToList();
            }
            vm.Comments = LookUp[null].ToList();
            return vm;
        }

        public async Task<PostListViewModel> GetAllPostAsync(PostListViewModel vm)
        {
            var count = await _postRepository.CountPostsAsync(vm.Keyword, vm.Category);

            var totalPages = (int)Math.Ceiling(count / (double)vm.PageSize);
            var page = vm.Page;

            if (page > totalPages && totalPages > 0)
            {
                page = totalPages;//超過就顯示最後一頁
            }

            var skip = (page - 1) * vm.PageSize;
            var posts = await _postRepository.GetPostAsync(vm.Keyword, vm.Category, skip, vm.PageSize);

            var postIds = posts.Select(p => p.Id).ToList();
            var commentCounts = await _postRepository.GetCommentCountsAsync(postIds);

            var items = posts.Select(p => new PostListItemViewModel
            {
                Id = p.Id,
                Title = p.Title,
                Category = p.Category,
                UserName = p.User.Name,
                CarName = p.Car != null ? p.Car.Brand + " " + p.Car.Model : null,
                CommentCount = commentCounts.GetValueOrDefault(p.Id),
                CreatedAt = p.CreatedAt
            }).ToList();

            return new PostListViewModel
            {
                Items = items,
                Keyword = vm.Keyword,
                Category = vm.Category,
                Page = page,
                TotalCount = count
            };
        }

        public async Task<bool> DeletePostAsync(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if(post == null)
            {
                return false;
            }
            await _postRepository.DeletePostByIdAsync(id);
            return true;
        }
    }
}
