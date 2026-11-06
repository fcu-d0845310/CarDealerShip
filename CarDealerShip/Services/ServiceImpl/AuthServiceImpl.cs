using CarDealerShip.Dtos.Auth;
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

        public async Task<AuthUserDto?> ValidateUserAsync(string email, string password)
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

            //回傳登入需要的資料
            return new AuthUserDto
            {
                Id = user.Id,
                Name = user.Name,
                Email = user.Email,
                RoleName = user.Role.Name
            };
        }

    }
}
