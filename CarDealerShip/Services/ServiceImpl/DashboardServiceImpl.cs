using CarDealerShip.Repository;
using CarDealerShip.ViewModels;


namespace CarDealerShip.Services.ServiceImpl
{
    public class DashboardServiceImpl : IDashboardService
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardServiceImpl(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }



        public async Task<DashboardViewModel> GetDataAsync()
        {
            DashboardViewModel vm = new DashboardViewModel();

            vm.CarCount = await _dashboardRepository.GetCarCountByBranchAsync();

            vm.OfferCount = await _dashboardRepository.GetOfferCountAsync();

            vm.TradeInCount = await _dashboardRepository.GetTradeInCountAsync();

            vm.OrderCount = await _dashboardRepository.GetOrderCountAsync();

            vm.ReservationCount = await _dashboardRepository.GetReservationCountAsync();

            vm.PostCount = await _dashboardRepository.GetPostCountAsync();


            DateTime monthStart = new DateTime(DateTime.Today.Year, DateTime.Today.Month, 1);
            DateTime nextMonthStart = monthStart.AddMonths(1);//下個月一號

            vm.MonthSale = await _dashboardRepository.GetSalesTotalAsync(monthStart, nextMonthStart);

            return vm;
        }
    }
}
