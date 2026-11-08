namespace CarDealerShip.Services.ServiceImpl
{
    public class ImageServiceImpl : IImageService//這裡處理伺服器裡的檔案
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ImageServiceImpl(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public void Delete(string imageUrl)
        {
            string imgPath = imageUrl.Trim('/');
            string path = Path.Combine(_webHostEnvironment.WebRootPath, imgPath);
            if (File.Exists(path))
            {
                File.Delete(path);
            }
        }

        public async Task<string> SaveAsync(IFormFile file)
        {
            string folder = Path.Combine(_webHostEnvironment.WebRootPath, "upload", "cars");//組合資料夾路徑wwwroot
            Directory.CreateDirectory(folder);//沒有路徑的話，新建立路徑
            string fileName = Guid.NewGuid() + Path.GetExtension(file.FileName).ToLower();//產生guid檔名加上副檔名
            string fullPath = Path.Combine(folder, fileName);//完整圖片路徑
            using var stream = new FileStream(fullPath, FileMode.Create);//建立檔案流確保能及時關閉
            await file.CopyToAsync(stream);//複製到伺服器的檔案流

            return $"/upload/cars/{fileName}";//回傳路徑
        }
            
    }
}
