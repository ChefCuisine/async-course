namespace AsyncCourse.Accounting.Api.Domain.Commands.Issues.Calculator;

public interface IIssueCalculator
{
    int GetPriceToAssign();
    int GetPriceToDone();
}

public class IssueCalculator : IIssueCalculator 
{
    private readonly Random random;

    public IssueCalculator()
    {
        random = new Random();
    }

    public int GetPriceToAssign()
    {
        return random.Next(10, 20);
    }

    public int GetPriceToDone()
    {
        return random.Next(20, 40);
    }
}