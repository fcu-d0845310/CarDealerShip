using CarDealerShip.Repository;
using CarDealerShip.ViewModels;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace CarDealerShip.Services.ServiceImpl
{
    public class CarServiceImpl : ICarService
    {
        private readonly ICarRepository _carRepository;

        public CarServiceImpl(ICarRepository carRepository)
        {
            _carRepository = carRepository;
        }

        public async Task<CarListViewModel> GetCarListAsync(CarListViewModel vm)
        {
            var count = await _carRepository.CountCarsAsync(vm.Brand, vm.Model, vm.Status, vm.BranchId);

            var totalPages = (int)Math.Ceiling(count / (double)vm.PageSize);
            var page = vm.Page;

            if (page > totalPages && totalPages > 0)
            {
                page = totalPages;//超過就顯示最後一頁
            }

            var skip = (page - 1) * vm.PageSize;
            var cars = await _carRepository.GetCarPageAsync(vm.Brand, vm.Model, vm.Status, vm.BranchId, skip, vm.PageSize);

            var items = cars.Select(c => new CarListItemViewModel
            {
                Id = c.Id,
                PrimaryImg = c.CarImages.FirstOrDefault()?.ImageUrl ?? string.Empty,//舊資料可能沒有主圖
                Brand = c.Brand,
                Model = c.Model,
                ProductionYear = c.ProductionYear,
                Mileage = c.Mileage,
                BranchName = c.Branch.Name,
                Status = c.Status,
                CostPrice = c.CostPrice,
                SellingPrice = c.SellingPrice
            }).ToList();

            var branches = await _carRepository.GetBranchesAsync();
            var brands = await _carRepository.GetBrandsAsync();

            return new CarListViewModel
            {
                Items = items,
                Brand = vm.Brand,
                Model = vm.Model,
                Status = vm.Status,
                BranchId = vm.BranchId,
                Brands = brands.Select(b => new SelectListItem { Text = b, Value = b }).ToList(),
                Branches = branches.Select(b => new SelectListItem { Text = b.Name, Value = b.Id.ToString() }).ToList(),
                Page = page,
                TotalCount = count
            };
        }
    }
}
