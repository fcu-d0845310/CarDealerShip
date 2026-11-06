using CarDealerShip.Models;

namespace CarDealerShip.Repository
{
    public interface ICarRepository
    {
        Task<int> CountCarsAsync(string? brand, string? model, string? status, int? branchId);
        Task<List<Car>> GetCarPageAsync(string? brand, string? model, string? status, int? branchId, int skip, int take);
        Task<List<Branch>> GetBranchesAsync();
        Task<List<string>> GetBrandsAsync();
        Task<Car?> GetCarByIdAsync(int id);
        Task<Car?> GetCarForUpdateAsync(int id);
        Task SaveUpdateChangesAsync();

        void RemoveCarImage(CarImage image);
    }
}
