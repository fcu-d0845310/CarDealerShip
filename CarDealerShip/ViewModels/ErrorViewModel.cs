namespace CarDealerShip.ViewModels
{
    //錯誤頁，只顯示追蹤編號，不顯示錯誤細節
    public class ErrorViewModel
    {
        public string? RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);
    }
}
