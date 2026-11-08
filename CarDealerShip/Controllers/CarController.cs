using CarDealerShip.Mappings;
using CarDealerShip.Services;
using CarDealerShip.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
namespace CarDealerShip.Controllers
{
    [Authorize(Roles = "Boss")]
    public class CarController : Controller
    {
        private readonly ICarService _carService;

        public CarController(ICarService carService)
        {
            _carService = carService;
        }

        public async Task<IActionResult> Index([Bind("Brand,Model,Status,BranchId,Page")] CarListViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                var filterOptions = await _carService.GetFilterOptionsAsync();
                vm.ApplyFilterOptions(filterOptions);
                return View(vm);
            }

            var query = vm.ToQueryDto();
            var result = await _carService.GetCarListAsync(query);
            var options = await _carService.GetFilterOptionsAsync();
            return View(result.ToListViewModel(query, options));
        }

        public async Task<IActionResult> Create(string? returnUrl)
        {
            var branches = await _carService.GetBranchOptionsAsync();
            CarCreateViewModel vm = new CarCreateViewModel
            {
                Branches = branches.ToSelectList(),
                ReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : null//只接受站內網址
            };
           
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(55 * 1024 * 1024)]//調高Kestrel整個請求的上限
        [RequestFormLimits(MultipartBodyLengthLimit = 55 * 1024 * 1024)]//調高請求大小上限
        public async Task<IActionResult> Create(CarCreateViewModel vm)
        {
            if(!ModelState.IsValid)
            {
                var branches = await _carService.GetBranchOptionsAsync();
                vm.Branches = branches.ToSelectList();
                vm.ReturnUrl = Url.IsLocalUrl(vm.ReturnUrl)? vm.ReturnUrl:null;//只接受站內網址
                return View(vm);
            }

            try 
            {
                await _carService.CreateCarAsync(vm.ToCreateDto(),vm.Images);
            }
            catch(IOException)
            {
                ModelState.AddModelError("", "圖片儲存失敗，請稍後再試");
                var branches = await _carService.GetBranchOptionsAsync();
                vm.Branches = branches.ToSelectList();
                vm.ReturnUrl = Url.IsLocalUrl(vm.ReturnUrl) ? vm.ReturnUrl : null;//只接受站內網址
                return View(vm);
            }
            if (Url.IsLocalUrl(vm.ReturnUrl))
            {
                return LocalRedirect(vm.ReturnUrl);//存檔後回到原本的列表頁面
            }
            return RedirectToAction("Index");
        }



        public async Task<IActionResult> Edit(int id, string? returnUrl)
        {
            var car = await _carService.GetCarDetailAsync(id);
            if(car == null)
            {
                //找不到車輛時回到原本的列表頁面，顯示錯誤訊息
                TempData["ErrorMessage"] = "找不到這台車輛";
                if (Url.IsLocalUrl(returnUrl))
                {
                    return LocalRedirect(returnUrl);
                }
                return RedirectToAction("Index");
            }
            var branches = await _carService.GetBranchOptionsAsync();
            var vm = car.ToFormViewModel(branches);
            vm.ReturnUrl = Url.IsLocalUrl(returnUrl) ? returnUrl : null;//只接受站內網址，會用在返回連結
            return View(vm);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute]int id, CarFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                //編輯輸入不合法，回編輯頁面
                var car = await _carService.GetCarDetailAsync(id);
                if (car == null)
                {
                    TempData["ErrorMessage"] = "找不到這台車";
                    if (Url.IsLocalUrl(vm.ReturnUrl))
                    {
                        return LocalRedirect(vm.ReturnUrl);
                    }
                    return RedirectToAction("Index");
                }
                var branches = await _carService.GetBranchOptionsAsync();
                vm.ApplyFormOptions(car, branches);
                vm.ReturnUrl = Url.IsLocalUrl(vm.ReturnUrl) ? vm.ReturnUrl : null;
                return View(vm);
            }

            if(!await _carService.UpdateCarAsync(id, vm.ToUpdateDto()))
            {
                TempData["ErrorMessage"] = "找不到這台車";
            }
            if (Url.IsLocalUrl(vm.ReturnUrl))
            {
                return LocalRedirect(vm.ReturnUrl);//存檔後回到原本的列表頁面
            }
            return RedirectToAction("Index");

        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> SetPrimaryImage([FromRoute] int id, int imageId, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "請求資料有誤";
                return RedirectToAction("Edit", new {id,returnUrl});
            }

            if (!await _carService.SetPrimaryImageAsync(id,imageId))
            {
                TempData["ErrorMessage"] = "找不到車輛或圖片不屬於該車";
                return RedirectToAction("Edit", new { id, returnUrl });
            }
            return RedirectToAction("Edit", new { id, returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteImage([FromRoute] int id, int imageId, string? returnUrl)
        {
            if (!ModelState.IsValid)
            {
                TempData["ErrorMessage"] = "請求資料有誤";
                return RedirectToAction("Edit", new {id,returnUrl});
            }

            if (!await _carService.DeleteImageAsync(id,imageId))
            {
                TempData["ErrorMessage"] = "找不到車輛或圖片不屬於該車";
                return RedirectToAction("Edit", new { id, returnUrl });
            }

            return RedirectToAction("Edit", new { id, returnUrl });
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [RequestSizeLimit(55 * 1024 * 1024)]//調高Kestrel整個請求的上限
        [RequestFormLimits(MultipartBodyLengthLimit = 55 * 1024 * 1024)]//調高請求大小上限
        public async Task<IActionResult> UploadImages([FromRoute] int id, CarImageUploadViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                //組合驗證錯誤訊息
                var errors = ModelState.Values.SelectMany(x => x.Errors).Select(x => x.ErrorMessage);
                TempData["ErrorMessage"] = string.Join("、", errors);
                return RedirectToAction("Edit", new { id, vm.ReturnUrl });
            }

            try
            {
                if (!await _carService.UploadImagesAsync(id, vm.Files))
                {
                    TempData["ErrorMessage"] = "找不到這台車輛";
                    return RedirectToAction("Edit", new { id,vm.ReturnUrl });
                }
            }
            catch (IOException)
            {
                //存檔失敗
                TempData["ErrorMessage"] = "圖片儲存失敗，請稍後再試";
                return RedirectToAction("Edit", new { id,vm.ReturnUrl });
            }

            return RedirectToAction("Edit", new { id, vm.ReturnUrl });
        }


    }
}
