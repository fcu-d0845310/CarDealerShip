using CarDealerShip.Models;
using CarDealerShip.Repository;

namespace CarDealerShip.Services.ServiceImpl
{
    public class CommentServiceImpl : ICommentService
    {
        private readonly ICommentRepository _commentRepository;

        public CommentServiceImpl(ICommentRepository commentRepository)
        {
            _commentRepository = commentRepository;
        }
        public async Task<bool> DeleteCommentByIdAsync(int id)
        {
            var comment = await _commentRepository.GetCommentByIdAsync(id);

            if(comment == null)
            {
                return false;
            }
           
            var commentChild = await _commentRepository.GetCommentByPostAsync(comment.PostId);

            var lookup = commentChild.ToLookup(c => c.ParentId);//依照父留言的id當作key建立(key : 多個comment)
            var toDelete = new List<Comment>();//確定要刪除的留言
            var stack = new Stack<Comment>();//尚未處理過的留言
            stack.Push(comment);

            while (stack.Count > 0)
            {
                var current = stack.Pop();
                toDelete.Add(current);
                foreach (var reply in lookup[current.Id])
                {
                    stack.Push(reply);
                }
            }

            await _commentRepository.DeleteCommentAsync(toDelete);
            return true;
        }

    }
}
