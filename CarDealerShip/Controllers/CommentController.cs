using CarDealerShip.Services;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerShip.Controllers
{
    [Authorize(Roles = "Boss")]
    public class CommentController : Controller
    {
        private readonly ICommentService _commentService;

        public CommentController(ICommentService commentService)
        {
            _commentService = commentService;
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, int postId, string? returnUrl)
        {
            if(!await _commentService.DeleteCommentByIdAsync(id))
            {
                //留言已不存在加上錯誤訊息
                TempData["ErrorMessage"] = "這則留言已經被刪除";
            }
            return RedirectToAction("PostDetail","Post",new { id = postId, returnUrl });//returnUrl 由 PostDetail 檢查
        }
    }
}
