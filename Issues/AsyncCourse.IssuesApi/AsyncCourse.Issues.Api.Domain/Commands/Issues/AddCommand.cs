using AsyncCourse.Issues.Api.Domain.Repositories;
using AsyncCourse.Issues.Api.Models.Issues;
using AsyncCourse.Template.Kafka.MessageBus;
using AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;
using Newtonsoft.Json;

namespace AsyncCourse.Issues.Api.Domain.Commands.Issues;

public interface IAddCommand
{
    Task AddAsync(Issue issue);
}

public class AddCommand : IAddCommand
{
    private readonly IIssueRepository issueRepository;
    private readonly ITemlateKafkaMessageBus messageBus;

    public AddCommand(IIssueRepository issueRepository, ITemlateKafkaMessageBus messageBus)
    {
        this.issueRepository = issueRepository;
        this.messageBus = messageBus;
    }

    public async Task AddAsync(Issue issue)
    {
        await issueRepository.AddAsync(issue);
        
        var kafkaAccount = Map(issue);
        var message = JsonConvert.SerializeObject(kafkaAccount);

        await messageBus.SendMessageAsync(Constants.IssueCreateTopic, message);
    }
    
    private static MessageBusIssue Map(Issue issue)
    {
        return new MessageBusIssue
        {
            Id = issue.Id,
            Title = issue.Title,
            Description = issue.Description,
            Status = issue.Status.ToString(),
            AccountId = issue.AccountId,
        };
    }
}