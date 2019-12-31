using EasyUowApplication.Aggregates;
using EasyUowApplication.EFCore;
using MiCake.Uow.Easy;
using System;
using MiCake.EFCore.Easy;

namespace EasyUowApplication.Repositories
{
    public class ItineraryRepository : EFRepository<UowAppDbContext, Itinerary, Guid>
    {
        public ItineraryRepository(IUnitOfWorkManager uowManager) : base(uowManager)
        {
        }

        public void Add(Itinerary itinerary)
        {
            DbContext.Set<Itinerary>().Add(itinerary);
        }
    }
}
