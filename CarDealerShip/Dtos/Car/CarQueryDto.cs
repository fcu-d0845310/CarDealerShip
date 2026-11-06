namespace CarDealerShip.Dtos.Car
{
    //車輛列表的查詢條件
    public class CarQueryDto
    {
        public string? Brand { get; set; }//品牌，完全比對
        public string? Model { get; set; }//型號，部分比對
        public string? Status { get; set; }//車輛狀態
        public int? BranchId { get; set; }//分店
        public int Page { get; set; } = 1;//要查詢的頁碼
    }
}
