using Mapster;
using MiCake.DDD.Domain;
using MiCake.Identity.Authentication;
using MiCakeDemoApplication.Domain.UserBoundary.Repositories;
using MiCakeDemoApplication.Dto.User;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;
using MiCakeApp = MiCakeDemoApplication.Domain.UserBoundary.Aggregates;

namespace MiCakeDemoApplication.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepo;
        private readonly IJwtSupporter _jwtSupporter;

        public UserController(IUserRepository userRepository, IJwtSupporter jwtSupporter)
        {
            _userRepo = userRepository;
            _jwtSupporter = jwtSupporter;
        }

        [HttpPost]
        public async Task<LoginResultDto> Login(LoginDto loginInfo)
        {
            CheckCode(loginInfo.Code);

            var user = await _userRepo.FindUserByPhone(loginInfo.Phone);

            if (user == null)
                return LoginResultDto.NoUser();

            var token = _jwtSupporter.CreateToken(user);
            return new LoginResultDto() { AccessToken = token, HasUser = true, UserInfo = user.Adapt<UserDto>() };
        }

        [HttpPost]
        public async Task<LoginResultDto> Register(RegisterUserDto registerInfo)
        {
            CheckCode(registerInfo.Code);

            var user = MiCakeApp.User.Create(registerInfo.Phone, registerInfo.Password, registerInfo.Name, registerInfo.Age);
            await _userRepo.AddUserAsync(user);

            var token = _jwtSupporter.CreateToken(user);

            return new LoginResultDto() { AccessToken = token, HasUser = true, UserInfo = user.Adapt<UserDto>() };
        }

        private void CheckCode(string code)
        {
            if (!code.ToLower().Equals("micake"))
                throw new DomainException($"验证码不正确");
        }
    }
}