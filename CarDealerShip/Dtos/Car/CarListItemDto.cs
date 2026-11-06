namespace CarDealerShip.Dtos.Car
{
    //車輛列表的一筆資料
    public class CarListItemDto
    {
        public int Id { get; set; }
        public string PrimaryImageUrl { get; set; } = null!;//主圖網址，沒有主圖時為空字串
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public int ProductionYear { get; set; }
        public int Mileage { get; set; }
        public string BranchName { get; set; } = null!;
        public string Status { get; set; } = null!;
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
    }
}
