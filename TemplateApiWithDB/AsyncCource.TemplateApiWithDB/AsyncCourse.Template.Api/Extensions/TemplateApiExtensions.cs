using AsyncCource.TemplateApiWithDB.Configuration;
using AsyncCourse.Core.Db.DbContextSupport;
using AsyncCourse.Core.WarmUp;
using AsyncCourse.Template.Api.Db;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;

namespace AsyncCource.TemplateApiWithDB.Extensions;

public static class TemplateApiExtensions
{
    public static IServiceCollection AddAsyncCourseProperties(this IServiceCollection services)
    {
        return services.AddSingleton(ReadSettingsJson());
    }
    
    public static IServiceCollection AddAsyncCourseDbContext(this IServiceCollection services)
    {
        return services
            .AddSingleton<TemplateApiDbContext>()
            .AddSingleton(typeof(IDbContextCreator<TemplateApiDbContext>), typeof(TemplateApiDbContextCreator))
            .AddSingleton(typeof(IDesignTimeDbContextFactory<TemplateApiDbContext>), typeof(AsyncCourseDesignTimeDbContextFactory))
            .AddSingleton<IWarmUp, TemplateApiDbWarmUp>();
    }

    private static TemplateApiApplicationSettings ReadSettingsJson()
    {
        using var reader = new StreamReader("Settings/template_api_settings.json");
        var json = reader.ReadToEnd();
        var configuration = JsonConvert.DeserializeObject<TemplateApiApplicationSettings>(json);
        return configuration;
    }
}