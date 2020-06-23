using MiCake.AspNetCore.Security;
using MiCake.Identity.Authentication;
using MiCakeDemoApplication.Utils;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using System.Threading.Tasks;

namespace MiCakeDemoApplication.Extensions
{
    public static class AuthenticationServiceCollectionExtension
    {
        /// <summary>
        /// 添加JwtBearer本地验证和微信小程序远程验证方案.
        /// </summary>
        public static AuthenticationBuilder AddWeChatAndJwtBearer(this IServiceCollection services, IConfiguration configuration)
        {
            //指定需要自动识别的user id 的 claim key.
            VerifyUserClaims.UserID = GlobalArgs.ClaimUserId;

            var seurityKey = Encoding.Default.GetBytes(configuration["JwtConfig:SecurityKey"]);

            //配置IJwtSupporter用于颁发token的配置，与下方JwtBearer配置相同.
            services.PostConfigure<MiCakeJwtOptions>(jwtOptions =>
            {
                jwtOptions.Audience = configuration["JwtConfig:Audience"];
                jwtOptions.Issuer = configuration["JwtConfig:Issuer"];
                jwtOptions.ExpirationMinutes = int.Parse(configuration["JwtConfig:ExpireDay"]) * 24 * 60;
                jwtOptions.SecurityKey = seurityKey;
            });

            //Add Authroize.配置微信小程序远程验证方案和JWT Bearer的本地验证验证方案.
            var result = services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
                                 .AddWeChatMiniProgram(wechatOptions =>
                                 {
                                     wechatOptions.WeChatAppId = configuration["WeChatMiniProgram:appid"];
                                     wechatOptions.WeChatSecret = configuration["WeChatMiniProgram:secret"];

                                     wechatOptions.SaveSessionKeyToCache = true;
                                     wechatOptions.CustomerLoginState += context =>
                                     {
                                         //使用重定向来进行验证用户并且颁发Token
                                         var currentUrl = $"WeChatUser/Login?key={context.SessionInfoKey}";
                                         context.HttpContext.Response.Redirect(currentUrl);

                                         return Task.CompletedTask;
                                     };
                                 })
                                 .AddJwtBearer(jwtOptions =>
                                 {
                                     jwtOptions.TokenValidationParameters = new TokenValidationParameters()
                                     {
                                         IssuerSigningKey = new SymmetricSecurityKey(seurityKey),
                                         ValidIssuer = configuration["JwtConfig:Issuer"],
                                         ValidAudience = configuration["JwtConfig:Audience"],
                                     };
                                 });

            return result;
        }
    }
}
