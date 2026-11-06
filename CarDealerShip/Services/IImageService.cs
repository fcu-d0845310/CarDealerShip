namespace CarDealerShip.Services
{
    public interface IImageService
    {
        Task<string> SaveAsync(IFormFile file);

        void Delete(string imageUrl);
    }
}
