namespace CarDealerShip.ViewModels
{
    public class DashboardViewModel
    {
        public Dictionary<string,int> CarCount { get; set; } = new Dictionary<string,int>();

        public int OfferCount{ get; set; }

        public int TradeInCount{  get; set; }

        public int OrderCount { get; set; }

        public int ReservationCount { get; set; }

        public int PostCount { get; set; }

        public decimal MonthSale {  get; set; }

        
    }
}
