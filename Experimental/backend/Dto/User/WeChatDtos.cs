namespace MiCakeDemoApplication.Dto.User
{
    public class WeChatLoginDto
    {
        public bool HasUser { get; set; }
        public string OpenSessionKey { get; set; }
        public string AccessToken { get; set; }
        public UserDto UserInfo { get; set; }

        public static WeChatLoginDto NoUser(string sessionKey) => new WeChatLoginDto() { HasUser = false, OpenSessionKey = sessionKey };
    }

    public class RegisterWeChatUserDto
    {
        public string SessionKey { get; set; }

        public string Name { get; set; }

        public string Phone { get; set; }

        public string Avatar { get; set; }

        public int Age { get; set; }
    }
}
