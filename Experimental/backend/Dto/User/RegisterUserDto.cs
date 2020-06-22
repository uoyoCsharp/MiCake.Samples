using System;

namespace MiCakeDemoApplication.Dto.User
{
    public class RegisterUserDto
    {
        public string Phone { get; set; }

        public string Code { get; set; }

        public string Password { get; set; }

        public string Name { get; set; }

        public string Avatar { get; set; }

        public int Age { get; set; }
    }


    public class RegisterResultDto
    {
        public bool Success { get; set; }

        public string ErrorMsg { get; set; }

        public Guid UserId { get; set; }

        public static RegisterResultDto RegisterSuccess(Guid userId)
        {
            return new RegisterResultDto()
            {
                Success = true,
                UserId = userId,
            };
        }

        public static RegisterResultDto RegisterFailed(string errorMsg)
        {
            return new RegisterResultDto()
            {
                Success = false,
                ErrorMsg = errorMsg,
            };
        }
    }
}
