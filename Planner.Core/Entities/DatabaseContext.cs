using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using Sorenlorentzen.Invokable;

namespace Planner.Core.Entities;

public class DatabaseContext : DbContext, IDatabaseContext
{
    public DatabaseContext(DbContextOptions<DatabaseContext> options) : base(options)
    { }
    
    public DbSet<Event> Events { get; set; }
    
    public DbSet<TEntity> GetDbSet<TEntity>() where TEntity : BaseEntity
    {
        return Set<TEntity>();
    }

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConverter>();
    }
}
