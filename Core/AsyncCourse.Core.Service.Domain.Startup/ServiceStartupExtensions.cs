using AsyncCourse.Core.Db.DbContextSupport;
using AsyncCourse.Core.WarmUp;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncCourse.Core.Service.Domain.Startup;

public static class ServiceStartupExtensions
{
    public static IServiceCollection AddAsyncCourseDomain(this IServiceCollection services)
    {
        return services
                .AddSingleton(typeof(IDbContextFactory<>), typeof(DbContextFactory<>))
                .AddSingleton<IStartupFilter, WarmUpFilter>()
            ;
    }
}