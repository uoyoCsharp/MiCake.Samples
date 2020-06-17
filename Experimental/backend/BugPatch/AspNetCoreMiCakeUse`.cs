using MiCake.AspNetCore.Identity;
using MiCake.AspNetCore.Security;
using MiCake.Core.Util.Converts;
using MiCake.Core.Util.Reflection;
using MiCake.Identity;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.DependencyInjection.Extensions;
using System.Linq;
using System.Security.Claims;

namespace MiCakeDemoApplication.BugPatch
{
    //version 0.3.0-alpha2 没有注入ICurrentMiCakeUser导致报错的bug
    public class AspNetCoreMiCakeUser<TKey> : CurrentMiCakeUser<TKey>, IAspNetCoreMiCakeUser
    {
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ClaimsPrincipal User => _httpContextAccessor.HttpContext.User;

        public AspNetCoreMiCakeUser(IHttpContextAccessor httpContextAccessor) : base()
        {
            _httpContextAccessor = httpContextAccessor;
        }

        public override TKey GetUserID()
        {
            var userIDClaim = _httpContextAccessor.HttpContext.User.Claims.FirstOrDefault(s => s.Type.Equals(VerifyUserClaims.UserID));

            if (userIDClaim == null)
                return default;

            var userId = ConvertHelper.ConvertValue<string, TKey>(userIDClaim.Value);
            return userId;
        }
    }

    public static class AddMiCakeUserExtesions
    {
        public static void AddCurrentMiCakeUser<TMiCakeUser>(this IServiceCollection services)
        {
            var userKeyType = TypeHelper.GetGenericArguments(typeof(TMiCakeUser), typeof(IMiCakeUser<>));

            var currentMiCakeUserType = typeof(ICurrentMiCakeUser<>).MakeGenericType(userKeyType);
            var aspnetCoreCurrentUser = typeof(AspNetCoreMiCakeUser<>).MakeGenericType(userKeyType);
            //add ICurrentMiCakeUser
            services.Replace(new ServiceDescriptor(typeof(ICurrentMiCakeUser), aspnetCoreCurrentUser, ServiceLifetime.Scoped));
            services.Replace(new ServiceDescriptor(currentMiCakeUserType, aspnetCoreCurrentUser, ServiceLifetime.Scoped));
        }
    }
}
