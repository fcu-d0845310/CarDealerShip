using CarDealerShip.Models;

namespace CarDealerShip.Services
{
    public interface IAuthService
    {
        //驗證，並更新登入時間
        Task<User?> ValidateUserAsync(string email, string password);
    }
}
