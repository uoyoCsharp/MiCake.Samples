using MiCake.DDD.Domain;
using MiCakeDemoApplication.Domain.UserBoundary.Aggregates;
using System.Threading.Tasks;

namespace MiCakeDemoApplication.Domain.UserBoundary.Repositories
{
    public interface IUserWithWechatRepository : IRepository<UserWithWechat, long>
    {
        public Task<long> GetUserIdWithOpenId(string OpenId);
    }
}
