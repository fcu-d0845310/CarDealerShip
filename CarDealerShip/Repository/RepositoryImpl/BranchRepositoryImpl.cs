using CarDealerShip.Constants;
using CarDealerShip.Data;
using CarDealerShip.Models;
using Microsoft.EntityFrameworkCore;
namespace CarDealerShip.Repository.RepositoryImpl
{
    public class BranchRepositoryImpl : IBranchRepository
    {
        private readonly AppDbContext _appDbContext;
        public BranchRepositoryImpl(AppDbContext appDbContext) 
        {
            _appDbContext = appDbContext;
        }

        public void AddBranch(Branch branch)
        {
            _appDbContext.Branches.Add(branch);
        }

        public async Task<Branch?> GetBranchByIdAsync(int id)
        {
            var b = await _appDbContext.Branches.AsNoTracking().Where(x => x.Id == id).FirstOrDefaultAsync();
            return b;
        }

        public async Task<List<Branch>> GetBranchesAsync()
        {
            return await _appDbContext.Branches.AsNoTracking()
                .OrderBy(x => x.CreatedAt)
                .ToListAsync();
                
        }

        public async Task<Branch?> GetBranchForUpdateAsync(int id)
        {
            var b = await _appDbContext.Branches.Where(x => x.Id == id).FirstOrDefaultAsync();
            return b;
        }

        public async Task<Dictionary<int, int>> GetInStockCount()
        {
            return await _appDbContext.Branches.AsNoTracking()
                .Select(x => new { BranchId = x.Id, count = x.Cars.Count(c => c.Status == CarStatus.InStock) })
                .ToDictionaryAsync(x => x.BranchId, x=> x.count);
        }

        public async Task SaveChangesAsync()
        {
            await _appDbContext.SaveChangesAsync();
        }
    }
}
