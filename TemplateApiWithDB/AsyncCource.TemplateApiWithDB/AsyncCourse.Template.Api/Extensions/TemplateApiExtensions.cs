using AsyncCource.TemplateApiWithDB.Configuration;
using AsyncCourse.Core.Db.DbContextSupport;
using AsyncCourse.Core.WarmUp;
using AsyncCourse.Template.Api.Db;
using AsyncCourse.Template.Api.Domain.Commands.Kafka.Create;
using AsyncCourse.Template.Api.Domain.Commands.Kafka.Get;
using AsyncCourse.Template.Api.Domain.Commands.TemplateDomain.Add;
using AsyncCourse.Template.Api.Domain.Commands.TemplateDomain.List;
using AsyncCourse.Template.Kafka.MessageBus;
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
    
    public static IServiceCollection AddKafkaBus(this IServiceCollection services)
    {
        return services
                .AddSingleton<ITemlateKafkaMessageBus, TemlateKafkaMessageBus>()
            ;
    }
    
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services
            .AddSingleton<IGetListCommand, GetListCommand>() // template domain commands
            .AddSingleton<IAddCommand, AddCommand>()
            .AddSingleton<ICreateKafkaMessageCommand, CreateKafkaMessageCommand>() // kafka commands
            .AddSingleton<IGetKafkaMessageCommand, GetKafkaMessageCommand>()
            ;
    }

    private static TemplateApiApplicationSettings ReadSettingsJson()
    {
        using var reader = new StreamReader("Settings/template_api_settings.json");
        var json = reader.ReadToEnd();
        var configuration = JsonConvert.DeserializeObject<TemplateApiApplicationSettings>(json);
        return configuration;
    }
}