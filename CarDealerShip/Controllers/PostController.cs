using CarDealerShip.Services;
using CarDealerShip.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerShip.Controllers
{
    [Authorize(Roles ="Boss")]
    public class PostController : Controller
    {
        private readonly IPostService _postService;

        public PostController(IPostService postService)
        {
            _postService = postService;
        }

        public async Task<IActionResult> Index([Bind("Keyword,Category,Page")] PostListViewModel vm)
        {
           
            if (!ModelState.IsValid)
            {
                return View(vm);
            }

            PostListViewModel returnVM = await _postService.GetAllPostAsync(vm);
            return View(returnVM);
        }

        public async Task<IActionResult> PostDetail(int id)
        {
            PostDetailViewModel? detail = await _postService.GetPostDetailAsync(id);
            if(detail == null)
            {
                return NotFound();
            }
            return View(detail);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id,[Bind("Keyword,Category,Page")] PostListViewModel vm)
        {
            bool isExist = await _postService.DeletePostAsync(id);
            
            if(!isExist)
            {
                return NotFound(id);
            }
            return RedirectToAction("Index",new { vm.Keyword, vm.Category, vm.Page });//刪除後回到原本的頁碼頁面
        }
    }
}
