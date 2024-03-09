using AsyncCourse.Auth.Api.Configuration;
using AsyncCourse.Auth.Api.Db;
using AsyncCourse.Auth.Api.Domain.Commands.Accounts;
using AsyncCourse.Auth.Api.Domain.Repositories;
using AsyncCourse.Core.Db.DbContextSupport;
using AsyncCourse.Core.WarmUp;
using AsyncCourse.Template.Kafka.MessageBus;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;

namespace AsyncCourse.Auth.Api.Extensions;

public static class AuthApiExtensions
{
    public static IServiceCollection AddAsyncCourseProperties(this IServiceCollection services)
    {
        return services.AddSingleton(ReadSettingsJson());
    }
    
    public static IServiceCollection AddAsyncCourseDbContext(this IServiceCollection services)
    {
        return services
            .AddSingleton<AuthApiDbContext>()
            .AddSingleton(typeof(IDbContextCreator<AuthApiDbContext>), typeof(AuthApiDbContextCreator))
            .AddSingleton(typeof(IDesignTimeDbContextFactory<AuthApiDbContext>), typeof(AsyncCourseDesignTimeDbContextFactory))
            .AddSingleton<IWarmUp, AuthApiDbWarmUp>();
    }
    
    public static IServiceCollection AddKafkaBus(this IServiceCollection services)
    {
        return services
                .AddSingleton<ITemlateKafkaMessageBus, TemlateKafkaMessageBus>()
            ;
    }
    
    public static IServiceCollection AddRepositories(this IServiceCollection services)
    {
        return services
                .AddSingleton<IAccountRepository, AccountRepository>()
            ;
    }
    
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services
            .AddSingleton<IGetListCommand, GetListCommand>() // auth domain commands
            .AddSingleton<IGetCommand, GetCommand>()
            .AddSingleton<IGetByLoginCommand, GetByLoginCommand>()
            .AddSingleton<IAddCommand, AddCommand>()
            .AddSingleton<IEditCommand, EditCommand>()
            ;
    }

    private static AuthApiApplicationSettings ReadSettingsJson()
    {
        using var reader = new StreamReader("Settings/auth_api_settings.json");
        var json = reader.ReadToEnd();
        var configuration = JsonConvert.DeserializeObject<AuthApiApplicationSettings>(json);
        return configuration;
    }
}