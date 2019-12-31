using Microsoft.EntityFrameworkCore;

namespace MiCake.EFCore.Easy
{
    internal interface IUowDbContextFactory<TDbCotnext>
        where TDbCotnext : DbContext
    {
        TDbCotnext CreateDbContext();
    }
}
