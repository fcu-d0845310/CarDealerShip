namespace CarDealerShip.ViewModels
{
    //分店列表的一筆資料
    public class BranchListItemViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Address { get; set; } = null!;
        public string Phone { get; set; } = null!;
        public int InstockCount { get; set; }
    }
}
