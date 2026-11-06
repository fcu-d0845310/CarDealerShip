namespace CarDealerShip.Dtos.Car
{
    //車輛的一張圖片
    public class CarImageDto
    {
        public int Id { get; set; }
        public string ImageUrl { get; set; } = null!;
        public bool IsPrimary { get; set; }//是否為主圖
    }
}
