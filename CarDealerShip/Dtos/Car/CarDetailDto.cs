namespace CarDealerShip.Dtos.Car
{
    //單一車輛的完整資料，編輯頁顯示用
    public class CarDetailDto
    {
        public int Id { get; set; }
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
        public List<CarImageDto> Images { get; set; } = new();//目前已有的圖片
    }
}
