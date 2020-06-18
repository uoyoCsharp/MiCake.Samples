using MiCake.EntityFrameworkCore.Repository;
using MiCake.EntityFrameworkCore.Uow;
using MiCakeDemoApplication.Domain.UserBoundary.Aggregates;
using MiCakeDemoApplication.Domain.UserBoundary.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;

namespace MiCakeDemoApplication.EFCore.Repositories
{
    public class UserRepository : EFRepository<MyDbContext, User, Guid>, IUserRepository
    {
        public UserRepository(IDbContextProvider<MyDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }

        public async Task<User> FindUserByPhone(string phone)
        {
            return await DbSet.FirstOrDefaultAsync(s => s.Phone.Equals(phone));
        }

        public Task AddUserAsync(User user)
        {
            DbSet.Add(user);
            return Task.CompletedTask;
        }
    }
}
