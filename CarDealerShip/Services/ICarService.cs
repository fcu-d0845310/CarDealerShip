using CarDealerShip.Dtos.Car;
using CarDealerShip.Dtos.Common;

namespace CarDealerShip.Services
{
    public interface ICarService
    {
        //依條件查詢車輛列表，頁碼超過最後一頁時改顯示最後一頁
        Task<PagedResult<CarListItemDto>> GetCarListAsync(CarQueryDto query);

        //列表篩選區的品牌與分店選項
        Task<CarFilterOptionsDto> GetFilterOptionsAsync();

        //編輯頁的分店選項
        Task<List<OptionDto>> GetBranchOptionsAsync();

        //查詢單一車輛，查無資料回傳 null
        Task<CarDetailDto?> GetCarDetailAsync(int id);

        //更新車輛資料，查無資料回傳 false
        Task<bool> UpdateCarAsync(int id, CarUpdateDto dto);

        //上傳車輛圖片
        Task<bool> UploadImagesAsync(int carId, List<IFormFile> files);

        //刪除車輛圖片
        Task<bool> DeleteImageAsync(int carId, int imageId);

        //設定主圖片
        Task<bool> SetPrimaryImageAsync(int carId, int imageId);
    }
}
