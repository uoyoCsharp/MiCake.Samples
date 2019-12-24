using EasyUowApplication.Aggregates;
using EasyUowApplication.EFCore;
using MiCake.Uow.Easy;
using System;
using MiCake.EFCore.Easy;

namespace EasyUowApplication.Repositories
{
    public class ItineraryRepository : EFRepository<Itinerary, Guid>
    {
        public ItineraryRepository(IUnitOfWorkManager unitOfWorkManager, UowAppDbContext dbContext) : base(unitOfWorkManager, dbContext)
        {
        }
    }
}
