using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Planner.Core.Entities;
using Sorenlorentzen.Invokable;

namespace Planner.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddPlannerCore(this IServiceCollection services, string connectionString)
    {
        services.AddDbContext<DatabaseContext>(opt => opt.UseNpgsql(connectionString));
        services.AddInvokable<DatabaseContext>();

        return services;
    }
}