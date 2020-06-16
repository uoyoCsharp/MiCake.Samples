namespace MiCakeDemoApplication.Dto
{
    public class WeChatLoginDto
    {
        public bool HasUser { get; set; }
        public string AccessToken { get; set; }

        public static WeChatLoginDto NoUser() => new WeChatLoginDto() { HasUser = false };

    }
}
