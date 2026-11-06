namespace CarDealerShip.Services.ServiceImpl
{
    public class ImageServiceImpl : IImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageServiceImpl(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        //圖片儲存尚未實作，之後討論圖片處理方式時再完成
        public Task<string> SaveAsync(IFormFile file)
        {
            throw new NotImplementedException();
        }
    }
}
