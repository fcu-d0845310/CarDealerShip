using CarDealerShip.Data;
using CarDealerShip.ViewModels;
using Microsoft.EntityFrameworkCore;


namespace CarDealerShip.Services.ServiceImpl
{
    public class DashboardServiceImpl : IDashboardService
    {
        private readonly AppDbContext _appDbContext;

        public DashboardServiceImpl(AppDbContext db)
        {
            _appDbContext = db;
        }

       

        public async Task<DashboardViewModel> GetDataAsync()
        {
            DashboardViewModel vm = new DashboardViewModel();

            vm.CarCount = await _appDbContext.Branches
                .Select(x => new { Name = x.Name, Count = x.Cars.Count() })
                .ToDictionaryAsync(x => x.Name, x => x.Count);

            vm.OfferCount = await _appDbContext.Offers.CountAsync();

            vm.TradeInCount = await _appDbContext.TradeInRequests.CountAsync();

            vm.OrderCount = await _appDbContext.Orders.CountAsync();

            vm.ReservationCount = await _appDbContext.Reservations.CountAsync();

            vm.PostCount = await _appDbContext.Posts.CountAsync();

            
            DateTime monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime nextMonthStart = monthStart.AddMonths(1);//下個月一號

            vm.MonthSale = await _appDbContext.SalesRecords
                .Where(x => x.SaleDate >= monthStart && x.SaleDate < nextMonthStart)
                .SumAsync(x => x.FinalPrice);

            return vm;
        }
    }
}
