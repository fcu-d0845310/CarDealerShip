namespace CarDealerShip.ViewModels
{
    public class CarListItemViewModel
    {
        public int Id { get; set; }
        public string PrimaryImg { get; set; } = null!;
        public string Brand {  get; set; } = null!;
        public string Model { get; set; } = null!;
        public int ProductionYear { get; set; }
        public int Mileage{ get; set; }
        public string BranchName {  get; set; } = null!;
        public string Status {  get; set; } = null!;
        public decimal CostPrice {  get; set; }
        public decimal SellingPrice {  get; set; }


    }
}
