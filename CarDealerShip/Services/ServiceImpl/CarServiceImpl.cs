using CarDealerShip.Dtos.Car;
using CarDealerShip.Dtos.Common;
using CarDealerShip.Models;
using CarDealerShip.Repository;

namespace CarDealerShip.Services.ServiceImpl
{
    public class CarServiceImpl : ICarService
    {
        private const int CarPageSize = 12;//車輛列表每頁筆數

        private readonly ICarRepository _carRepository;
        private readonly IImageService _imageService;
        public CarServiceImpl(ICarRepository carRepository, IImageService imageService)
        {
            _carRepository = carRepository;
            _imageService = imageService;
        }

        public async Task<CarDetailDto?> GetCarDetailAsync(int id)
        {
            var target = await _carRepository.GetCarByIdAsync(id);
            if(target == null)
            {
                return null;
            }

            return new CarDetailDto
            {
                Id = target.Id,
                BranchId = target.BranchId,
                Brand = target.Brand,
                Model = target.Model,
                Color = target.Color,
                ProductionYear = target.ProductionYear,
                Mileage = target.Mileage,
                Displacement = target.Displacement,
                CostPrice = target.CostPrice,
                SellingPrice = target.SellingPrice,
                Status = target.Status,
                Description = target.Description,
                Images = target.CarImages.Select(i => new CarImageDto
                {
                    Id = i.Id,
                    ImageUrl = i.ImageUrl,
                    IsPrimary = i.IsPrimary
                }).ToList()
            };
        }

        public async Task<PagedResult<CarListItemDto>> GetCarListAsync(CarQueryDto query)
        {
            var count = await _carRepository.CountCarsAsync(query.Brand, query.Model, query.Status, query.BranchId);

            var totalPages = (int)Math.Ceiling(count / (double)CarPageSize);
            var page = query.Page;

            if (page > totalPages && totalPages > 0)
            {
                page = totalPages;//超過就顯示最後一頁
            }

            var skip = (page - 1) * CarPageSize;
            var cars = await _carRepository.GetCarPageAsync(query.Brand, query.Model, query.Status, query.BranchId, skip, CarPageSize);

            var items = cars.Select(c => new CarListItemDto
            {
                Id = c.Id,
                PrimaryImageUrl = c.CarImages.FirstOrDefault()?.ImageUrl ?? string.Empty,//舊資料可能沒有主圖
                Brand = c.Brand,
                Model = c.Model,
                ProductionYear = c.ProductionYear,
                Mileage = c.Mileage,
                BranchName = c.Branch.Name,
                Status = c.Status,
                CostPrice = c.CostPrice,
                SellingPrice = c.SellingPrice
            }).ToList();

            return new PagedResult<CarListItemDto>
            {
                Items = items,
                Page = page,
                PageSize = CarPageSize,
                TotalCount = count
            };
        }

        public async Task<CarFilterOptionsDto> GetFilterOptionsAsync()
        {
            var brands = await _carRepository.GetBrandsAsync();
            var branches = await GetBranchOptionsAsync();

            return new CarFilterOptionsDto
            {
                Brands = brands,
                Branches = branches
            };
        }

        public async Task<List<OptionDto>> GetBranchOptionsAsync()
        {
            var branches = await _carRepository.GetBranchesAsync();

            return branches.Select(b => new OptionDto
            {
                Id = b.Id,
                Name = b.Name
            }).ToList();
        }

        public async Task<bool> UpdateCarAsync(int id, CarUpdateDto dto)
        {
            var car = await _carRepository.GetCarForUpdateAsync(id);
            if(car == null)
            {
                return false;
            }
            car.BranchId = dto.BranchId;
            car.Brand = dto.Brand;
            car.Model = dto.Model;
            car.Color = dto.Color;
            car.ProductionYear = dto.ProductionYear;
            car.Mileage = dto.Mileage;
            car.Displacement = dto.Displacement;
            car.CostPrice = dto.CostPrice;
            car.SellingPrice = dto.SellingPrice;
            car.Status = dto.Status;
            car.Description = dto.Description;



            await _carRepository.SaveUpdateChangesAsync();
            return true;
        }

        public async Task<bool> UploadImagesAsync(int carId, List<IFormFile> files)
        {
            var car = await _carRepository.GetCarForUpdateAsync(carId);

            if(car == null)
            {
                return false;
            }

            bool hasPrimary = car.CarImages.Any(x => x.IsPrimary == true);
            var savedUrls = new List<string>();//已存檔的網址失敗時用來刪檔

            try
            {
                foreach(var image in files)
                {
                    string url = await _imageService.SaveAsync(image);
                    savedUrls.Add(url);

                    car.CarImages.Add(new CarImage
                    {
                        ImageUrl = url,
                        IsPrimary = !hasPrimary
                    });
                    hasPrimary = true;
                }

                await _carRepository.SaveUpdateChangesAsync();
            }
            catch
            {
                //存檔或寫入資料庫失敗，刪掉這次已存的檔案
                foreach (var url in savedUrls)
                {
                    _imageService.Delete(url);
                }
                throw;
            }
            return true;
        }

        public async Task<bool> DeleteImageAsync(int carId, int imageId)
        {
            var car = await _carRepository.GetCarForUpdateAsync(carId);
            if (car == null)
            {
                return false;
            }
            var target = car.CarImages.Where(x => x.Id == imageId).FirstOrDefault();

            if(target == null)
            {
                return false;
            }

            string url = target.ImageUrl;
            bool isPrimary = target.IsPrimary;
            _carRepository.RemoveCarImage(target);
            
            if(isPrimary)
            {
                var tmp = car.CarImages.Where(img => img.Id != imageId).OrderBy(x => x.CreatedAt).FirstOrDefault();
                if(tmp != null)
                {
                    tmp.IsPrimary = true;
                }
            }
            await _carRepository.SaveUpdateChangesAsync();
            _imageService.Delete(url);
            return true;
        }

        public async Task<bool> SetPrimaryImageAsync(int carId, int imageId)
        {
            var car = await _carRepository.GetCarForUpdateAsync(carId);
            if (car == null)
            {
                return false;
            }

            var target = car.CarImages.FirstOrDefault(x => x.Id == imageId);
            if (target == null)
            {
                return false;
            }

            //先全部取消再設定，確保只有一張主圖
            foreach (var img in car.CarImages)
            {
                img.IsPrimary = false;
            }
            target.IsPrimary = true;

            await _carRepository.SaveUpdateChangesAsync();
            return true;
        }
    }
}
