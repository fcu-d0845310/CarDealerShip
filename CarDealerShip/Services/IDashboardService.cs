using CarDealerShip.ViewModels;

namespace CarDealerShip.Services
{
    public interface IDashboardService
    {
        Task<DashboardViewModel> GetDataAsync();
    }
}
