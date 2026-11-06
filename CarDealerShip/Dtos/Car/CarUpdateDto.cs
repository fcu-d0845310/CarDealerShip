namespace CarDealerShip.Dtos.Car
{
    //編輯車輛時要寫回資料庫的欄位，圖片的修改之後另外處理
    public class CarUpdateDto
    {
        public int BranchId { get; set; }
        public string Brand { get; set; } = null!;
        public string Model { get; set; } = null!;
        public string Color { get; set; } = null!;
        public int ProductionYear { get; set; }
        public int Mileage { get; set; }
        public int Displacement { get; set; }
        public decimal CostPrice { get; set; }
        public decimal SellingPrice { get; set; }
        public string Status { get; set; } = null!;
        public string? Description { get; set; }
    }
}
