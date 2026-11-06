namespace CarDealerShip.Dtos.Common
{
    //下拉選單的一個選項，只放純資料，轉成 SelectListItem 由 Mappings 負責
    public class OptionDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
    }
}
