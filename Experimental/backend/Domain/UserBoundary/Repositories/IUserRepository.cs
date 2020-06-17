using MiCake.DDD.Domain;
using MiCakeDemoApplication.Domain.UserBoundary.Aggregates;
using System;

namespace MiCakeDemoApplication.Domain.UserBoundary.Repositories
{
    public interface IUserRepository : IRepository<User, Guid>
    {

    }
}
