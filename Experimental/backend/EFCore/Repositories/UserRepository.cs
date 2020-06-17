using MiCake.EntityFrameworkCore.Repository;
using MiCake.EntityFrameworkCore.Uow;
using MiCakeDemoApplication.Domain.UserBoundary.Aggregates;
using MiCakeDemoApplication.Domain.UserBoundary.Repositories;
using System;

namespace MiCakeDemoApplication.EFCore.Repositories
{
    public class UserRepository : EFRepository<MyDbContext, User, Guid>, IUserRepository
    {
        public UserRepository(IDbContextProvider<MyDbContext> dbContextProvider) : base(dbContextProvider)
        {
        }
    }
}
