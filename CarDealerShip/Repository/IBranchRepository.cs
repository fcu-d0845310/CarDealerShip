using CarDealerShip.Models;

namespace CarDealerShip.Repository
{
    public interface IBranchRepository
    {
        Task<List<Branch>> GetBranchesAsync();
        Task<Dictionary<int,int>> GetInStockCount();
        Task<Branch?> GetBranchByIdAsync(int id);
        Task<Branch?> GetBranchForUpdateAsync(int id);
        void AddBranch(Branch branch);
        Task SaveChangesAsync();
    }
}
