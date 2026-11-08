using CarDealerShip.Dtos.Branch;

namespace CarDealerShip.Services
{
    public interface IBranchService
    {
        //分店列表，包含每間分店的在庫車輛數
        Task<List<BranchListItemDto>> GetBranchListAsync();

        //查詢單一分店，查無資料回傳 null
        Task<BranchDetailDto?> GetBranchByIdAsync(int id);

        //新增分店
        Task CreateBranchAsync(BranchSaveDto branch);

        //更新分店資料，查無資料回傳 false
        Task<bool> UpdateBranchAsync(int id, BranchSaveDto dto);
    }
}
