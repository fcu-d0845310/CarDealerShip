namespace CarDealerShip.Repository
{
    public interface IDashboardRepository
    {
        Task<Dictionary<string, int>> GetCarCountByBranchAsync();

        Task<int> GetOfferCountAsync();

        Task<int> GetTradeInCountAsync();

        Task<int> GetOrderCountAsync();

        Task<int> GetReservationCountAsync();

        Task<int> GetPostCountAsync();

        //加總指定期間金額
        Task<decimal> GetSalesTotalAsync(DateTime start, DateTime end);
    }
}
