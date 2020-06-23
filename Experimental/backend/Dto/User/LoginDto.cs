namespace MiCakeDemoApplication.Dto.User
{
    public class LoginDto
    {
        public string Phone { get; set; }

        public string Password { get; set; }

        public string Code { get; set; }
    }

    public class LoginResultDto
    {
        public bool HasUser { get; set; }

        public string AccessToken { get; set; }

        public UserDto UserInfo { get; set; }

        public static LoginResultDto NoUser() => new LoginResultDto() { HasUser = false };
    }
}
