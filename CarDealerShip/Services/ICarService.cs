using CarDealerShip.ViewModels;

namespace CarDealerShip.Services
{
    public interface ICarService
    {
        Task<CarListViewModel> GetCarListAsync(CarListViewModel vm);
    }
}
