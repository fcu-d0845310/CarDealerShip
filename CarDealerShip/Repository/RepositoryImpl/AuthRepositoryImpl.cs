using CarDealerShip.Data;
using CarDealerShip.Models;
using Microsoft.EntityFrameworkCore;

namespace CarDealerShip.Repository.RepositoryImpl
{
    public class AuthRepositoryImpl : IAuthRepository
    {
        private readonly AppDbContext _appDbContext;

        public AuthRepositoryImpl(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User?> GetBossByEmailAsync(string email)
        {
            return await _appDbContext.Users
                .AsNoTracking()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email && x.Role.Name == "Boss");
        }

        public async Task UpdateLastLoginAsync(int userId, DateTime loginAt)
        {
            await _appDbContext.Users
                .Where(x => x.Id == userId)
                .ExecuteUpdateAsync(s => s.SetProperty(x => x.LastLoginAt, loginAt));
        }
    }
}
