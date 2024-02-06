using AsyncCourse.Core.Db;
using AsyncCourse.Core.Db.Configuration;
using Microsoft.Extensions.DependencyInjection;

namespace AsyncCourse.Template.Api.Db.Configuration;

public static class AsyncCourseDbServiceStartupExtensions
{
    public static IServiceCollection AddAsyncCourseDbSettings<TAppProperties>(this IServiceCollection services)
        where TAppProperties : IAsyncCourseAppPropertiesWithDb
    {
        return services
            .AddSingleton(typeof(IDbSettings), typeof(AsyncCourseDbSettings<TAppProperties>));
    }
}