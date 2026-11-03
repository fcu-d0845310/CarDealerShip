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
            var vm = await _postRepository.GetPostById(id);
            if ( vm == null)
            {
                return null;
            }

            var Comments = await _postRepository.GetCommentByPostIdAsync(id);
            var LookUp = Comments.ToLookup(c => c.ParentId);//根據parentId轉換成字典型態
            foreach (var c in Comments)
            {
                c.Replies = LookUp[c.Id].ToList();
            }
            vm.Comments = LookUp[null].ToList();
            return vm;
        }

        public async Task<PostListViewModel> GetAllPost(PostListViewModel vm)
        {
            return await _postRepository.GetAllPost(vm);
        }
    }
}
