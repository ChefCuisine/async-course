using AsyncCourse.Accounting.Api.Domain.Commands.Issues.Calculator;
using AsyncCourse.Accounting.Api.Domain.Repositories.Issues;
using AsyncCourse.Accounting.Api.Models.Issues;

namespace AsyncCourse.Accounting.Api.Domain.Commands.Issues;

public interface IAddIssueCommand
{
    Task AddAsync(AccountingIssue issue);
}

public class AddIssueCommand : IAddIssueCommand
{
    private readonly IIssueRepository issueRepository;
    private readonly IIssueCalculator issueCalculator;

    public AddIssueCommand(IIssueRepository issueRepository, IIssueCalculator issueCalculator)
    {
        this.issueRepository = issueRepository;
        this.issueCalculator = issueCalculator;
    }

    public async Task AddAsync(AccountingIssue issue)
    {
        await issueRepository.AddAsync(issue);
        
        // получаем рандомную цену из калькулятора
        
        // смотрим на кого назначена
        // создаем событие на создание транзакции
    }
}