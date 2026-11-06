using CarDealerShip.Dtos.Common;

namespace CarDealerShip.Dtos.Car
{
    //車輛列表篩選區需要的選項
    public class CarFilterOptionsDto
    {
        public List<string> Brands { get; set; } = new();//資料庫中現有的品牌
        public List<OptionDto> Branches { get; set; } = new();//所有分店
    }
}
