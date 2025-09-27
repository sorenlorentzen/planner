using System;
using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Query;
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

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        var types = modelBuilder.Model.GetEntityTypes();
        foreach (var entityType in types)
        {
            if (entityType.ClrType.IsAssignableTo(typeof(BaseEntity)))
            {
                var parameter = Expression.Parameter(entityType.ClrType);
                var body = ReplacingExpressionVisitor.Replace(filterExpr.Parameters.First(), parameter, filterExpr.Body);
                var lambdaExpression = Expression.Lambda(body, parameter);

                // set filter
                entityType.SetQueryFilter(lambdaExpression);
            }
        }

    }
    private Expression<Func<BaseEntity, bool>> filterExpr = b => b.Deleted == null;

    protected override void ConfigureConventions(ModelConfigurationBuilder configurationBuilder)
    {
        configurationBuilder.Properties<DateTimeOffset>().HaveConversion<DateTimeOffsetConverter>();
    }
}
