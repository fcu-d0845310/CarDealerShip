namespace CarDealerShip.Dtos.Auth
{
    //驗證通過的使用者資料，刻意不包含密碼
    public class AuthUserDto
    {
        public int Id { get; set; }
        public string Name { get; set; } = null!;
        public string Email { get; set; } = null!;
        public string RoleName { get; set; } = null!;
    }
}
