using AsyncCourse.Accounting.Api.Models.Balances;
using AsyncCourse.Accounting.Api.Models.Balances.Models;

namespace AsyncCourse.Accounting.Api.Models.Mappings;

public static class AccountBalanceMapping
{
    public static AccountBalanceInfoModel MapFromDomainResult(AccountBalanceInfo balanceInfo)
    {
        return new AccountBalanceInfoModel
        {
            AccountId = balanceInfo.AccountId,
            Surname = balanceInfo.Surname,
            Name = balanceInfo.Name,
            Date = balanceInfo.Date,
            Total = balanceInfo.Total,
            Transactions = balanceInfo.Transactions.Select(Map).ToList()
        };

        TransactionBalanceInfoModel Map(TransactionBalanceInfo transactionBalanceInfo)
        {
            return new TransactionBalanceInfoModel
            {
                CreatedAt = transactionBalanceInfo.CreatedAt,
                Type = transactionBalanceInfo.Type.ToString(),
                Amount = transactionBalanceInfo.Amount,
                IssueInfo = MapIssueInfo(transactionBalanceInfo.IssueInfo)
            };
        }

        IssueBalanceInfoModel MapIssueInfo(IssueBalanceInfo issueBalanceInfo)
        {
            return new IssueBalanceInfoModel
            {
                JiraId = issueBalanceInfo.JiraId,
                Title = issueBalanceInfo.Title,
                Status = issueBalanceInfo.Status.ToString()
            };
        }
    }

    public static ManagementBalanceInfoModel MapFromDomainResult(ManagementBalanceInfo balanceInfo)
    {
        return new ManagementBalanceInfoModel
        {
            Total = balanceInfo.Total,
            Days = balanceInfo.Days.Select(Map).ToList()
        };
        
        ManagementBalanceDayInfoModel Map(ManagementBalanceDayInfo dayInfo)
        {
            return new ManagementBalanceDayInfoModel
            {
                Date = dayInfo.Date,
                Total = dayInfo.Total
            };
        }
    }
}