using CarDealerShip.Models;

namespace CarDealerShip.Repository
{
    public interface IAuthRepository
    {
        // 依 Email 查詢老闆帳號，查無資料回傳 null
        Task<User?> GetBossByEmailAsync(string email);

        // 更新使用者的最後登入時間
        Task UpdateLastLoginAsync(int userId, DateTime loginAt);
    }
}
