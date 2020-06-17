using MiCake;
using MiCake.Core.Modularity;
using MiCakeDemoApplication.Domain.UserBoundary.Repositories;
using MiCakeDemoApplication.EFCore.Repositories;

namespace MiCakeDemoApplication
{
    public class MyEntryModule : MiCakeModule
    {
        public override void ConfigServices(ModuleConfigServiceContext context)
        {
            context.RegisterRepository<IUserRepository, UserRepository>();
            context.RegisterRepository<IUserWithWechatRepository, UserWithWechatRepository>();
        }
    }
}
