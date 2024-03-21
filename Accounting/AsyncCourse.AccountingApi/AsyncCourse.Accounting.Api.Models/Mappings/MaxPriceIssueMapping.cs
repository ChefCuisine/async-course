using AsyncCourse.Accounting.Api.Models.Analytics;
using AsyncCourse.Accounting.Api.Models.Analytics.Models;

namespace AsyncCourse.Accounting.Api.Models.Mappings;

public static class MaxPriceIssueMapping
{
    public static MaxPriceIssueModel MapFromDomainResult(MaxPriceIssue maxPriceIssue)
    {
        return new MaxPriceIssueModel
        {
            Id = maxPriceIssue.Id,
            TransactionId = maxPriceIssue.TransactionId,
            IssueId = maxPriceIssue.IssueId,
            Amount = maxPriceIssue.Amount,
            Date = maxPriceIssue.Date
        };
    }
}