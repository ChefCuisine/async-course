using Microsoft.Extensions.DependencyInjection;

namespace AsyncCourse.Core.Db.Configuration;

public static class AsyncCourseDbServiceStartupExtensions
{
    public static IServiceCollection AddAsyncCourseDbSettings<TAppProperties>(this IServiceCollection services)
        where TAppProperties : IAsyncCourseAppPropertiesWithDb
    {
        return services
            .AddSingleton(typeof(IDbSettings), typeof(AsyncCourseDbSettings<TAppProperties>));
    }
}