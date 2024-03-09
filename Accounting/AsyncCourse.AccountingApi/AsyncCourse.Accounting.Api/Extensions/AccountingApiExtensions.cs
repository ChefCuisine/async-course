using AsyncCourse.Accounting.Api.Db;
using AsyncCourse.Accounting.Api.Domain.Commands.Accounts;
using AsyncCourse.Accounting.Api.Domain.Repositories.Accounts;
using AsyncCourse.AccountingApi.Configuration;
using AsyncCourse.Auth.Api.Client;
using AsyncCourse.Core.Db.DbContextSupport;
using AsyncCourse.Core.WarmUp;
using AsyncCourse.Template.Kafka.MessageBus;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;
using Vostok.Logging.Abstractions;

namespace AsyncCourse.AccountingApi.Extensions;

public static class AccountingApiExtensions
{
    public static IServiceCollection AddAsyncCourseProperties(this IServiceCollection services)
    {
        return services.AddSingleton(ReadSettingsJson());
    }
    
    public static IServiceCollection AddAsyncCourseDbContext(this IServiceCollection services)
    {
        return services
            .AddSingleton<AccountingApiDbContext>()
            .AddSingleton(typeof(IDbContextCreator<AccountingApiDbContext>), typeof(AccountingApiDbContextCreator))
            .AddSingleton(typeof(IDesignTimeDbContextFactory<AccountingApiDbContext>), typeof(AsyncCourseDesignTimeDbContextFactory))
            .AddSingleton<IWarmUp, AccountingApiDbWarmUp>();
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
                .AddSingleton<IAddAccountCommand, AddAccountCommand>() // accounting account commands
                .AddSingleton<IUpdateAccountCommand, UpdateAccountCommand>()
            ;
    }
    
    public static IServiceCollection AddExternalClients(this IServiceCollection services)
    {
        return services.AddSingleton<IAuthApiClient>(_ => 
            {
                return new AuthApiClient(AuthApiLocalAddress.Get(), new SilentLog());
            })
            ;
    }

    private static AccountingApiApplicationSettings ReadSettingsJson()
    {
        using var reader = new StreamReader("Settings/accounting_api_settings.json");
        var json = reader.ReadToEnd();
        var configuration = JsonConvert.DeserializeObject<AccountingApiApplicationSettings>(json);
        return configuration;
    }
}