using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Planner.Core.Entities;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<DatabaseContext>
{
    public DatabaseContext CreateDbContext(string[] args)
    {
        var builder = new DbContextOptionsBuilder<DatabaseContext>();
        builder.UseNpgsql();

        return new DatabaseContext(builder.Options);
    }
}