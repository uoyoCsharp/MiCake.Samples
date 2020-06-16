using MiCake.EntityFrameworkCore.Repository;
using MiCake.EntityFrameworkCore.Uow;
using MiCakeDemoApplication.Domain.UserBoundary.Aggregates;
using MiCakeDemoApplication.Domain.UserBoundary.Repositories;
using System.Linq;
using System.Threading.Tasks;

namespace MiCakeDemoApplication.EFCore.Repositories
{
    public class UserWithWechatRepository : EFRepository<MyDbContext, UserWithWechat, long>, IUserWithWechatRepository
    {
        public UserWithWechatRepository(IDbContextProvider<MyDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public Task<long> GetUserIdWithOpenId(string OpenId)
        {
            var model = DbSet.FirstOrDefault(s => s.WeChatOpenID.Equals(OpenId));
            return Task.FromResult(model == null ? default : model.UserID);
        }
    }
}
