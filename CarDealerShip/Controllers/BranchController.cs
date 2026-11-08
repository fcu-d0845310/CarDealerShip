using CarDealerShip.Mappings;
using CarDealerShip.Services;
using CarDealerShip.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CarDealerShip.Controllers
{
    [Authorize(Roles = "Boss")]
    public class BranchController : Controller
    {
        private readonly IBranchService _branchService;

        public BranchController(IBranchService branchService)
        {
            _branchService = branchService;
        }

        public async Task<IActionResult> Index()
        {
            return View(await BuildIndexViewModelAsync(new BranchFormViewModel(), false));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(Prefix = "Form")] BranchFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                //驗證失敗回列表頁並重新打開視窗
                return View("Index", await BuildIndexViewModelAsync(vm, true));
            }

            await _branchService.CreateBranchAsync(vm.ToSaveDto());
            return RedirectToAction("Index");
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit([FromRoute] int id, [Bind(Prefix = "Form")] BranchFormViewModel vm)
        {
            if (!ModelState.IsValid)
            {
                vm.Id = id;//視窗要知道是編輯哪一間
                return View("Index", await BuildIndexViewModelAsync(vm, true));
            }

            if (!await _branchService.UpdateBranchAsync(id, vm.ToSaveDto()))
            {
                TempData["ErrorMessage"] = "找不到這間分店";
            }
            return RedirectToAction("Index");
        }

        //組合列表頁，表格資料每次都重新查詢
        private async Task<BranchIndexViewModel> BuildIndexViewModelAsync(BranchFormViewModel form, bool showForm)
        {
            var branches = await _branchService.GetBranchListAsync();
            return new BranchIndexViewModel
            {
                Branches = branches.Select(b => b.ToViewModel()).ToList(),
                Form = form,
                ShowForm = showForm
            };
        }
    }
}
