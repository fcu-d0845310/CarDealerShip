using CarDealerShip.Models;

namespace CarDealerShip.Services
{
    public interface IAuthService
    {
        // 驗證後臺登入帳密，成功回傳使用者，失敗回傳 null
        Task<User?> ValidateUserAsync(string email, string password);
    }
}
