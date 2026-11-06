using CarDealerShip.Dtos.Common;
using CarDealerShip.Dtos.Post;
using CarDealerShip.Repository;

namespace CarDealerShip.Services.ServiceImpl
{
    public class PostServiceImpl : IPostService
    {
        private const int PostPageSize = 10;//貼文列表每頁筆數

        private readonly IPostRepository _postRepository;

        public PostServiceImpl(IPostRepository repo)
        {
            _postRepository = repo;
        }

        public async Task<PostDetailDto?> GetPostDetailAsync(int id)
        {
            var post = await _postRepository.GetPostByIdAsync(id);
            if (post == null)
            {
                return null;
            }

            var dto = new PostDetailDto
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
            var Comments = commentEntities.Select(c => new CommentDto
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
            dto.Comments = LookUp[null].ToList();
            return dto;
        }

        public async Task<PagedResult<PostListItemDto>> GetAllPostAsync(PostQueryDto query)
        {
            var count = await _postRepository.CountPostsAsync(query.Keyword, query.Category);

            var totalPages = (int)Math.Ceiling(count / (double)PostPageSize);
            var page = query.Page;

            if (page > totalPages && totalPages > 0)
            {
                page = totalPages;//超過就顯示最後一頁
            }

            var skip = (page - 1) * PostPageSize;//跳過資料數
            var posts = await _postRepository.GetPostAsync(query.Keyword, query.Category, skip, PostPageSize);

            var postIds = posts.Select(p => p.Id).ToList();
            var commentCounts = await _postRepository.GetCommentCountsAsync(postIds);

            var items = posts.Select(p => new PostListItemDto
            {
                Id = p.Id,
                Title = p.Title,
                Category = p.Category,
                UserName = p.User.Name,
                CarName = p.Car != null ? p.Car.Brand + " " + p.Car.Model : null,
                CommentCount = commentCounts.GetValueOrDefault(p.Id),
                CreatedAt = p.CreatedAt
            }).ToList();

            return new PagedResult<PostListItemDto>
            {
                Items = items,
                Page = page,
                PageSize = PostPageSize,
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
