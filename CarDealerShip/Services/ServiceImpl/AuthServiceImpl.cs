using CarDealerShip.Data;
using CarDealerShip.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;

namespace CarDealerShip.Services.ServiceImpl
{
    public class AuthServiceImpl : IAuthService
    {

        private readonly AppDbContext _appDbContext;

        public AuthServiceImpl(AppDbContext appDbContext)
        {
            _appDbContext = appDbContext;
        }

        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            var user = await _appDbContext.Users
                .AsNoTracking()
                .Include(x => x.Role)
                .FirstOrDefaultAsync(x => x.Email == email && x.Role.Name == "Boss");

            if (user == null || user.Password.IsNullOrEmpty())
            {
                return null;
            }
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }
            return user;
        }   
    }
}
