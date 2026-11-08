using CarDealerShip.Dtos.Branch;
using CarDealerShip.Models;
using CarDealerShip.Repository;

namespace CarDealerShip.Services.ServiceImpl
{
    public class BranchServiceImpl : IBranchService
    {
        private readonly IBranchRepository _branchRepository;

        public BranchServiceImpl(IBranchRepository branchRepository)
        {
            _branchRepository = branchRepository;
        }

        public async Task CreateBranchAsync(BranchSaveDto branch)
        {
            Branch b = new Branch();
            b.Name = branch.Name;
            b.Address = branch.Address;
            b.Phone = branch.Phone;

            _branchRepository.AddBranch(b);
            await _branchRepository.SaveChangesAsync();
        }

        public async Task<BranchDetailDto?> GetBranchByIdAsync(int id)
        {
            var b = await _branchRepository.GetBranchByIdAsync(id);
            if (b == null)
            {
                return null;
            }

            return new BranchDetailDto
            {
                Id = b.Id,
                Name = b.Name,
                Address = b.Address,
                Phone = b.Phone
            };
        }

        public async Task<List<BranchListItemDto>> GetBranchListAsync()
        {
            var branches = await _branchRepository.GetBranchesAsync();
            var counts = await _branchRepository.GetInStockCount();

            return branches.Select(b => new BranchListItemDto
            {
                Id = b.Id,
                Name = b.Name,
                Address = b.Address,
                Phone = b.Phone,
                InstockCount = counts[b.Id]
            }).ToList();
        }

        public async Task<bool> UpdateBranchAsync(int id, BranchSaveDto branch)
        {
            var b = await _branchRepository.GetBranchForUpdateAsync(id);
            if (b == null)
            {
                return false;
            }

            b.Name = branch.Name;
            b.Address = branch.Address;
            b.Phone = branch.Phone;

            await _branchRepository.SaveChangesAsync();
            return true;
        }
    }
}
