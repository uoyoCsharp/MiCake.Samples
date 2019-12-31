using MiCake.DDD.Domain;
using MiCake.Uow.Easy;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace MiCake.EFCore.Easy
{
    public class EFRepository<TDbContext, TAggregateRoot, TKey> : IReadOnlyRepository<TAggregateRoot, TKey>
        where TAggregateRoot : class, IAggregateRoot<TKey>
        where TDbContext : DbContext
    {
        public virtual TDbContext DbContext
        {
            get
            {
                return _dbContextFactory.CreateDbContext();
            }
        }

        private readonly IUnitOfWorkManager _uowManager;
        private IUowDbContextFactory<TDbContext> _dbContextFactory;

        public EFRepository(IUnitOfWorkManager uowManager)
        {
            _uowManager = uowManager;

            _dbContextFactory = new UowDbContextFactory<TDbContext>(_uowManager);
        }

        public virtual TAggregateRoot Find(TKey ID)
        {
            return DbContext.Find<TAggregateRoot>(ID);
        }

        public virtual Task<TAggregateRoot> FindAsync(TKey ID, CancellationToken cancellationToken = default)
        {
            return DbContext.FindAsync<TAggregateRoot>(ID, cancellationToken).AsTask();
        }

        public virtual long GetCount()
        {
            return DbContext.Set<TAggregateRoot>().LongCountAsync().Result;
        }
    }
}
