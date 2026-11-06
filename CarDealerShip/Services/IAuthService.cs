using CarDealerShip.Dtos.Auth;

namespace CarDealerShip.Services
{
    public interface IAuthService
    {
        //驗證，並更新登入時間，驗證失敗回傳 null
        Task<AuthUserDto?> ValidateUserAsync(string email, string password);
    }
}
