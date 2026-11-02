using CarDealerShip.Data;
using Microsoft.EntityFrameworkCore;

namespace CarDealerShip.Repository.RepositoryImpl
{
    public class DashboardRepositoryImpl : IDashboardRepository
    {
        private readonly AppDbContext _appDbContext;

        public DashboardRepositoryImpl(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<Dictionary<string, int>> GetCarCountByBranchAsync()
        {
            return await _appDbContext.Branches
                .Select(x => new { Name = x.Name, Count = x.Cars.Count() })
                .ToDictionaryAsync(x => x.Name, x => x.Count);
        }

        public async Task<int> GetOfferCountAsync()
        {
            return await _appDbContext.Offers.CountAsync();
        }

        public async Task<int> GetTradeInCountAsync()
        {
            return await _appDbContext.TradeInRequests.CountAsync();
        }

        public async Task<int> GetOrderCountAsync()
        {
            return await _appDbContext.Orders.CountAsync();
        }

        public async Task<int> GetReservationCountAsync()
        {
            return await _appDbContext.Reservations.CountAsync();
        }

        public async Task<int> GetPostCountAsync()
        {
            return await _appDbContext.Posts.CountAsync();
        }

        public async Task<decimal> GetSalesTotalAsync(DateTime start, DateTime end)
        {
            return await _appDbContext.SalesRecords
                .Where(x => x.SaleDate >= start && x.SaleDate < end)
                .SumAsync(x => x.FinalPrice);
        }
    }
}
