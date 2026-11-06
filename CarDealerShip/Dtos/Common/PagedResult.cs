namespace CarDealerShip.Dtos.Common
{
    //分頁查詢的結果，車輛列表與貼文列表共用
    public class PagedResult<T>
    {
        public List<T> Items { get; set; } = new();//當頁的資料
        public int Page { get; set; }//實際顯示的頁碼，超過最後一頁時會被修正成最後一頁
        public int PageSize { get; set; }//每頁筆數，由 Service 決定
        public int TotalCount { get; set; }//符合條件的總筆數
    }
}
