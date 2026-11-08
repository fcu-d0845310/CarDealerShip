using CarDealerShip.Constants;
using CarDealerShip.Data;
using CarDealerShip.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealerShip.Repository.RepositoryImpl
{
    public class CarRepositoryImpl : ICarRepository
    {
        private readonly AppDbContext _appDbContext;

        public CarRepositoryImpl(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        //計算符合條件的車輛數
        public async Task<int> CountCarsAsync(string? brand, string? model, string? status, int? branchId)
        {
            return await BuildCarQuery(brand, model, status, branchId).CountAsync();
        }

        //查詢當頁車輛，圖片只載入主圖
        public async Task<List<Car>> GetCarPageAsync(string? brand, string? model, string? status, int? branchId, int skip, int take)
        {
            return await BuildCarQuery(brand, model, status, branchId)
                .Include(c => c.Branch)
                .Include(c => c.CarImages.Where(i => i.IsPrimary))
                .OrderByDescending(c => c.CreatedAt)
                .ThenByDescending(c => c.Id)
                .Skip(skip)
                .Take(take)
                .ToListAsync();
        }
        //分店選項選單
        public async Task<List<Branch>> GetBranchesAsync()
        {
            return await _appDbContext.Branches.AsNoTracking()
                .OrderBy(b => b.Id)
                .ToListAsync();
        }

        //品牌選項選單
        public async Task<List<string>> GetBrandsAsync()
        {
            return await _appDbContext.Cars.AsNoTracking()
                .Select(c => c.Brand)
                .Distinct()
                .OrderBy(b => b)
                .ToListAsync();
        }


        public async Task<Car?> GetCarByIdAsync(int id)
        {
            return await _appDbContext.Cars.AsNoTracking()
                .Include(c => c.CarImages)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task<Car?> GetCarForUpdateAsync(int id)
        {
            return await _appDbContext.Cars
                .Include(c => c.CarImages)
                .Where(c => c.Id == id)
                .FirstOrDefaultAsync();
        }

        public async Task SaveUpdateChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }

        //標記要刪除的圖片
        public void RemoveCarImage(CarImage image)
        {
            _appDbContext.CarImages.Remove(image);
        }
        //標記要新增的車輛
        public void AddCar(Car car)
        {
            _appDbContext.Cars.Add(car);
        }

        //篩選條件
        private IQueryable<Car> BuildCarQuery(string? brand, string? model, string? status, int? branchId)
        {
            var query = _appDbContext.Cars.AsNoTracking();

            if (!string.IsNullOrEmpty(brand))
            {
                query = query.Where(c => c.Brand == brand);
            }

            if (!string.IsNullOrEmpty(model))
            {
                var trimmedModel = model.Trim();
                query = query.Where(c => c.Model.Contains(trimmedModel));
            }

            if (!string.IsNullOrEmpty(status))
            {
                query = query.Where(c => c.Status == status);
            }
            else
            {
                query = query.Where(c => c.Status != CarStatus.Removed);
            }

            if (branchId != null)
            {
                query = query.Where(c => c.BranchId == branchId.Value);
            }

            return query;
        }
    }
}
