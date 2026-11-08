namespace CarDealerShip.ViewModels
{
    //分店列表頁，表格加上新增與編輯共用的彈出視窗
    public class BranchIndexViewModel
    {
        public List<BranchListItemViewModel> Branches { get; set; } = new();
        public BranchFormViewModel Form { get; set; } = new();//彈出視窗的表單
        public bool ShowForm { get; set; }//驗證失敗時進頁面就打開視窗
    }
}
