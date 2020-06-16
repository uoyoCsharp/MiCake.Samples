using MiCake.DDD.Domain;
using MiCakeDemoApplication.Domain.UserBoundary.Aggregates;

namespace MiCakeDemoApplication.Domain.UserBoundary.Repositories
{
    public interface IUserRepository : IRepository<User, long>
    {

    }
}
