using CarDealerShip.Models;
using CarDealerShip.Repository;

namespace CarDealerShip.Services.ServiceImpl
{
    public class AuthServiceImpl : IAuthService
    {

        private readonly IAuthRepository _authRepository;

        public AuthServiceImpl(IAuthRepository authRepository)
        {
            _authRepository = authRepository;
        }

        public async Task<User?> ValidateUserAsync(string email, string password)
        {
            var user = await _authRepository.GetBossByEmailAsync(email);

            if (user == null || string.IsNullOrEmpty(user.Password))
            {
                return null;
            }
            if (!BCrypt.Net.BCrypt.Verify(password, user.Password))
            {
                return null;
            }
            await _authRepository.UpdateLastLoginAsync(user.Id, DateTime.Now);
            return user;
        }

    }
}
