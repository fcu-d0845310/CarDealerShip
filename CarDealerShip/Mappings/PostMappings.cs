using CarDealerShip.Dtos.Common;
using CarDealerShip.Dtos.Post;
using CarDealerShip.ViewModels;

namespace CarDealerShip.Mappings
{
    //貼文功能的 DTO 與 ViewModel 轉換
    public static class PostMappings
    {
        //把列表頁送來的搜尋與篩選條件轉成查詢 DTO
        public static PostQueryDto ToQueryDto(this PostListViewModel vm)
        {
            return new PostQueryDto
            {
                Keyword = vm.Keyword,
                Category = vm.Category,
                Page = vm.Page
            };
        }

        //把查詢結果與原本的搜尋條件組成列表頁的 ViewModel
        public static PostListViewModel ToListViewModel(this PagedResult<PostListItemDto> result, PostQueryDto query)
        {
            return new PostListViewModel
            {
                Keyword = query.Keyword,
                Category = query.Category,
                Items = result.Items.Select(i => i.ToViewModel()).ToList(),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
        }

        //列表的一筆資料
        public static PostListItemViewModel ToViewModel(this PostListItemDto dto)
        {
            return new PostListItemViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Category = dto.Category,
                UserName = dto.UserName,
                CarName = dto.CarName,
                CommentCount = dto.CommentCount,
                CreatedAt = dto.CreatedAt
            };
        }

        //貼文詳細頁，ReturnUrl 由 Controller 檢查後再設定
        public static PostDetailViewModel ToViewModel(this PostDetailDto dto)
        {
            return new PostDetailViewModel
            {
                Id = dto.Id,
                Title = dto.Title,
                Content = dto.Content,
                Category = dto.Category,
                CreatedAt = dto.CreatedAt,
                AuthorName = dto.AuthorName,
                CarName = dto.CarName,
                Comments = dto.Comments.Select(c => c.ToViewModel()).ToList()
            };
        }

        //留言連同底下所有回覆一起轉換
        public static CommentViewModel ToViewModel(this CommentDto dto)
        {
            return new CommentViewModel
            {
                Id = dto.Id,
                Content = dto.Content,
                UserName = dto.UserName,
                CreatedAt = dto.CreatedAt,
                Replies = dto.Replies.Select(r => r.ToViewModel()).ToList()
            };
        }
    }
}
