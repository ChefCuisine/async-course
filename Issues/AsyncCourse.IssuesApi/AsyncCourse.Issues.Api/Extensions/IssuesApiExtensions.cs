using AsyncCourse.Auth.Api.Client;
using AsyncCourse.Core.Db.DbContextSupport;
using AsyncCourse.Core.WarmUp;
using AsyncCourse.Issues.Api.Configuration;
using AsyncCourse.Issues.Api.Db;
using AsyncCourse.Issues.Api.Domain.Commands.Issues;
using AsyncCourse.Issues.Api.Domain.Commands.Issues.Assigner;
using AsyncCourse.Issues.Api.Domain.Commands.IssuesAccounts;
using AsyncCourse.Issues.Api.Domain.Commands.OutboxEvents;
using AsyncCourse.Issues.Api.Domain.Repositories.Accounts;
using AsyncCourse.Issues.Api.Domain.Repositories.Issues;
using AsyncCourse.Issues.Api.Domain.Repositories.OutboxEvents;
using AsyncCourse.Template.Kafka.MessageBus;
using Microsoft.EntityFrameworkCore.Design;
using Newtonsoft.Json;
using Vostok.Logging.Console;

namespace AsyncCourse.Issues.Api.Extensions;

public static class IssuesApiExtensions
{
    public static IServiceCollection AddAsyncCourseProperties(this IServiceCollection services)
    {
        return services.AddSingleton(ReadSettingsJson());
    }
    
    public static IServiceCollection AddAsyncCourseDbContext(this IServiceCollection services)
    {
        return services
            .AddSingleton<IssuesApiDbContext>()
            .AddSingleton(typeof(IDbContextCreator<IssuesApiDbContext>), typeof(IssuesApiDbContextCreator))
            .AddSingleton(typeof(IDesignTimeDbContextFactory<IssuesApiDbContext>), typeof(AsyncCourseDesignTimeDbContextFactory))
            .AddSingleton<IWarmUp, IssuesApiDbWarmUp>();
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
                .AddSingleton<IIssueRepository, IssueRepository>()
                .AddSingleton<IIssueAccountRepository, IssueAccountRepository>()
                .AddSingleton<IIssueOutboxEventRepository, IssueOutboxEventRepository>()
            ;
    }
    
    public static IServiceCollection AddCommands(this IServiceCollection services)
    {
        return services
            .AddSingleton<IGetListCommand, GetListCommand>() // issues domain commands
            .AddSingleton<IAddCommand, AddCommand>()
            .AddSingleton<IGetCommand, GetCommand>()
            .AddSingleton<IDoneCommand, DoneCommand>()
            .AddSingleton<IReassignCommand, ReassignCommand>()
            .AddSingleton<IAddIssueAccountCommand, AddIssueAccountCommand>() // issues account commands
            .AddSingleton<IUpdateIssueAccountCommand, UpdateIssueAccountCommand>()
            .AddSingleton<IReadOneIssueOutboxEventCommand, ReadOneIssueOutboxEventIssueOutboxEventCommand>() // issues event commands
            .AddSingleton<IRemoveIssueOutboxEventCommand, RemoveIssueOutboxEventIssueOutboxEventCommand>()
            .AddSingleton<IIssueAssigner, IssueAssigner>() // other services using within commands
            ;
    }
    
    public static IServiceCollection AddExternalClients(this IServiceCollection services)
    {
        return services.AddSingleton<IAuthApiClient>(_ => 
            {
                return new AuthApiClient(AuthApiLocalAddress.Get(), new ConsoleLog());
            })
            ;
    }

    private static IssuesApiApplicationSettings ReadSettingsJson()
    {
        using var reader = new StreamReader("Settings/issues_api_settings.json");
        var json = reader.ReadToEnd();
        var configuration = JsonConvert.DeserializeObject<IssuesApiApplicationSettings>(json);
        return configuration;
    }
}