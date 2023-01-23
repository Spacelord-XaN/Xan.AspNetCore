using Microsoft.EntityFrameworkCore;
using Xan.AspNetCore.EntityFrameworkCore.ChangeTracking;
using Xan.AspNetCore.Mvc.Abstractions;
using Xan.Extensions;

namespace Xan.AspNetCore.EntityFrameworkCore;

public static class DbContextExtensions
{
    public static void AddTimestampHandler(this DbContext dbContext, IClock clock)
    {
        ArgumentNullException.ThrowIfNull(dbContext);
        ArgumentNullException.ThrowIfNull(clock);

        dbContext.SavingChanges += (sender, args) =>
        {
            foreach (IHasTimestamps entity in dbContext.ChangeTracker.Entries().AddedEntities<IHasTimestamps>())
            {
                entity.CreatedAt = clock.GetCurrentDateTime();
                entity.UpdatedAt = clock.GetCurrentDateTime();
            }

            foreach (IHasTimestamps entity in dbContext.ChangeTracker.Entries().ModifedEntities<IHasTimestamps>())
            {
                entity.UpdatedAt = clock.GetCurrentDateTime();
            }
        };
    }
}
