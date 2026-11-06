using CarDealerShip.Dtos.Car;
using CarDealerShip.Dtos.Common;
using CarDealerShip.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealerShip.Mappings
{
    //車輛功能的 DTO 與 ViewModel 轉換
    public static class CarMappings
    {
        //把列表頁送來的篩選條件轉成查詢 DTO
        public static CarQueryDto ToQueryDto(this CarListViewModel vm)
        {
            return new CarQueryDto
            {
                Brand = vm.Brand,
                Model = vm.Model,
                Status = vm.Status,
                BranchId = vm.BranchId,
                Page = vm.Page
            };
        }

        //把查詢結果、原本的篩選條件與篩選選項組成列表頁的 ViewModel
        public static CarListViewModel ToListViewModel(this PagedResult<CarListItemDto> result, CarQueryDto query, CarFilterOptionsDto options)
        {
            var vm = new CarListViewModel
            {
                Brand = query.Brand,
                Model = query.Model,
                Status = query.Status,
                BranchId = query.BranchId,
                Items = result.Items.Select(i => i.ToViewModel()).ToList(),
                Page = result.Page,
                PageSize = result.PageSize,
                TotalCount = result.TotalCount
            };
            vm.ApplyFilterOptions(options);
            return vm;
        }

        //把篩選選項填入列表頁的下拉選單，直接修改傳入的 ViewModel，保留使用者原本輸入的篩選值
        public static void ApplyFilterOptions(this CarListViewModel vm, CarFilterOptionsDto options)
        {
            vm.Brands = options.Brands.Select(b => new SelectListItem { Text = b, Value = b }).ToList();
            vm.Branches = options.Branches.ToSelectList();
        }

        //列表的一筆資料
        public static CarListItemViewModel ToViewModel(this CarListItemDto dto)
        {
            return new CarListItemViewModel
            {
                Id = dto.Id,
                PrimaryImg = dto.PrimaryImageUrl,
                Brand = dto.Brand,
                Model = dto.Model,
                ProductionYear = dto.ProductionYear,
                Mileage = dto.Mileage,
                BranchName = dto.BranchName,
                Status = dto.Status,
                CostPrice = dto.CostPrice,
                SellingPrice = dto.SellingPrice
            };
        }

        //把車輛資料與分店選項組成編輯頁的 ViewModel
        public static CarFormViewModel ToFormViewModel(this CarDetailDto dto, IEnumerable<OptionDto> branches)
        {
            var vm = new CarFormViewModel
            {
                Id = dto.Id,
                BranchId = dto.BranchId,
                Brand = dto.Brand,
                Model = dto.Model,
                Color = dto.Color,
                ProductionYear = dto.ProductionYear,
                Mileage = dto.Mileage,
                Displacement = dto.Displacement,
                CostPrice = dto.CostPrice,
                SellingPrice = dto.SellingPrice,
                Status = dto.Status,
                Description = dto.Description
            };
            vm.ApplyFormOptions(dto, branches);
            return vm;
        }

        //把現有圖片與分店選項填入編輯頁，直接修改傳入的 ViewModel，保留使用者原本輸入的內容
        public static void ApplyFormOptions(this CarFormViewModel vm, CarDetailDto dto, IEnumerable<OptionDto> branches)
        {
            vm.ExistingImages = dto.Images.Select(i => i.ToViewModel()).ToList();
            vm.Branches = branches.ToSelectList();
        }

        //車輛的一張圖片
        public static CarImageViewModel ToViewModel(this CarImageDto dto)
        {
            return new CarImageViewModel
            {
                Id = dto.Id,
                ImageUrl = dto.ImageUrl,
                IsPrimary = dto.IsPrimary
            };
        }

        //把編輯表單轉成要更新的資料，呼叫前必須先通過 ModelState 驗證
        public static CarUpdateDto ToUpdateDto(this CarFormViewModel vm)
        {
            return new CarUpdateDto
            {
                BranchId = vm.BranchId!.Value,//分店有 Required 驗證，通過驗證後一定有值
                Brand = vm.Brand,
                Model = vm.Model,
                Color = vm.Color,
                ProductionYear = vm.ProductionYear,
                Mileage = vm.Mileage,
                Displacement = vm.Displacement,
                CostPrice = vm.CostPrice,
                SellingPrice = vm.SellingPrice,
                Status = vm.Status,
                Description = vm.Description
            };
        }
    }
}
