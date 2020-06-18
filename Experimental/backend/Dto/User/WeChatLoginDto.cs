namespace MiCakeDemoApplication.Dto.User
{
    public class WeChatLoginDto
    {
        public bool HasUser { get; set; }
        public string OpenSessionKey { get; set; }
        public string AccessToken { get; set; }

        public static WeChatLoginDto NoUser(string sessionKey) => new WeChatLoginDto() { HasUser = false, OpenSessionKey = sessionKey };
    }
}
