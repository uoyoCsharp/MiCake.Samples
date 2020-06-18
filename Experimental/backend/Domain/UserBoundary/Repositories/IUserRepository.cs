using MiCake.DDD.Domain;
using MiCakeDemoApplication.Domain.UserBoundary.Aggregates;
using System;
using System.Threading.Tasks;

namespace MiCakeDemoApplication.Domain.UserBoundary.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {
        public Task<User> FindUserByPhone(string phone);

        public Task AddUserAsync(User user);
    }
}
