using MiCake.Authentication.MiniProgram.WeChat;
using MiCake.Core.Util;
using MiCake.Identity.Authentication;
using MiCakeDemoApplication.Domain.UserBoundary.Aggregates;
using MiCakeDemoApplication.Domain.UserBoundary.Repositories;
using MiCakeDemoApplication.Dto;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Threading.Tasks;

namespace MiCakeDemoApplication.Controllers.Authentication
{
    [ApiController]
    [Route("[controller]/[action]")]
    public class WeChatUserController : ControllerBase
    {
        private readonly IJwtSupporter _jwtSupporter;
        private readonly IWeChatSessionInfoStore _weChatSessionStore;
        private readonly IUserWithWechatRepository _wechatRepository;
        private readonly IUserRepository _userRepository;

        public WeChatUserController(
            IJwtSupporter jwtSupporter,
            IWeChatSessionInfoStore weChatSessionInfo,
            IUserWithWechatRepository wechatUserRepository,
            IUserRepository userRepository)
        {
            _jwtSupporter = jwtSupporter;
            _weChatSessionStore = weChatSessionInfo;
            _userRepository = userRepository;
        }

        [HttpGet]
        public async Task<WeChatLoginDto> Login(string key)
        {
            CheckValue.NotNullOrWhiteSpace(key, nameof(key));

            var weChatSessionInfo = await _weChatSessionStore.GetSessionInfo(key) ?? throw new ArgumentException("没有找到匹配的微信密匙信息");
            var anyUser = await _wechatRepository.GetUserIdWithOpenId(weChatSessionInfo.OpenId);

            if (anyUser == default)
            {
                return WeChatLoginDto.NoUser();
            }

            var user = await _userRepository.FindAsync(anyUser);
            var token = _jwtSupporter.CreateToken(user);

            return new WeChatLoginDto() { AccessToken = token, HasUser = true };
        }

        [HttpPost]
        public async Task<WeChatLoginDto> RegisterUser(RegisterWeChatUserDto userDto)
        {
            CheckValue.NotNullOrWhiteSpace(userDto.SessionKey, "SessionKey");

            var weChatSessionInfo = await _weChatSessionStore.GetSessionInfo(userDto.SessionKey) ?? throw new ArgumentException("没有找到匹配的微信密匙信息");
            var newUser = new User(userDto.Name, userDto.Avatar, userDto.Age);

            var user = await _userRepository.AddAndReturnAsync(newUser);
            await _wechatRepository.AddAsync(new UserWithWechat(user.Id, weChatSessionInfo.OpenId));

            var token = _jwtSupporter.CreateToken(user);
            return new WeChatLoginDto() { AccessToken = token, HasUser = true };
        }
    }
}