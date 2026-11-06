using CarDealerShip.Mappings;
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

            //擴充方法
            var query = vm.ToQueryDto();

            var result = await _postService.GetAllPostAsync(query);
            return View(result.ToListViewModel(query));
        }

        public async Task<IActionResult> PostDetail(int id, string? returnUrl)
        {
            var detail = await _postService.GetPostDetailAsync(id);
            if(detail == null)
            {
                //找不到貼文時回到原本的列表頁面
                TempData["ErrorMessage"] = "找不到這篇貼文，可能已被刪除";
                if (Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction("Index");
            }
            var vm = detail.ToViewModel();
            vm.ReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : null;//只接受站內網址
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Delete(int id, string? returnUrl)
        {
            bool isExist = await _postService.DeletePostAsync(id);

            if(!isExist)
            {
                //貼文已不存在時加上訊息
                TempData["ErrorMessage"] = "這篇貼文已經被刪除";
            }
            if (Url.IsLocalUrl(returnUrl))
            {
                return LocalRedirect(returnUrl);//刪除後回到原本的列表頁面
            }
            return RedirectToAction("Index");
        }
    }
}
