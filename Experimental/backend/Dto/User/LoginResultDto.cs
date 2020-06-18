﻿namespace MiCakeDemoApplication.Dto.User
{
    public class LoginResultDto
    {
        public bool HasUser { get; set; }

        public string AccessToken { get; set; }

        public UserDto UserInfo { get; set; }

        public static LoginResultDto NoUser() => new LoginResultDto() { HasUser = false };
    }
}
