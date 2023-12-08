using Microsoft.EntityFrameworkCore;
using Xan.AspNetCore.TestWebApp.Models.Crud;
using Xan.Extensions;
using Xan.AspNetCore.EntityFrameworkCore;

namespace Xan.AspNetCore.TestWebApp.Data;

public class TestWebAppDbContext
    : DbContext
{
    public TestWebAppDbContext(DbContextOptions<TestWebAppDbContext> options)
        : base(options)
    {
        this.AddTimestampHandler(new SystemClock());
    }

    public DbSet<ShipEntity> Ships => Set<ShipEntity>();
}
