using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Accounting.Api.Models.Balances;
using AsyncCourse.Accounting.Api.Models.Issues;
using AsyncCourse.Accounting.Api.Models.OutboxEvents;
using AsyncCourse.Client;
using Vostok.Clusterclient.Core;
using Vostok.Clusterclient.Core.Model;
using Vostok.Logging.Abstractions;

namespace AsyncCourse.Accounting.Api.Client;

public class AccountingApiClient : RootClientBase, IAccountingApiClient
{
    public AccountingApiClient(Uri uri, ILog log) : base(uri, log)
    {
    }

    public AccountingApiClient(ClusterClientSetup setup, ILog log) : base(setup, log)
    {
    }

    public async Task<OperationResult<bool>> SaveAccountAsync(AccountingAccount account)
    {
        var request = Request
            .Post("/accounting-account/save")
            .WithJsonContent(account);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> UpdateAccountAsync(AccountingAccount account)
    {
        var request = Request
            .Post("/accounting-account/update")
            .WithJsonContent(account);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> SaveIssueAsync(AccountingIssue issue)
    {
        var request = Request
            .Post("/accounting-issue/save")
            .WithJsonContent(issue);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> ReassignIssueAsync(AccountingBusinessChangedIssue issue)
    {
        var request = Request
            .Post("/accounting-issue/reassign")
            .WithJsonContent(issue);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }
    
    public async Task<OperationResult<bool>> CloseIssueAsync(AccountingBusinessChangedIssue issue)
    {
        var request = Request
            .Post("/accounting-issue/close")
            .WithJsonContent(issue);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }
    
    public async Task<OperationResult<TransactionOutboxEvent>> ReadTransactionEventAsync()
    {
        var request = Request
            .Get("/transactions-event/read-one");

        var operationResult = await SendRequestAsync<TransactionOutboxEvent>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> DeleteTransactionEventAsync(Guid id)
    {
        var request = Request
            .Delete($"/transactions-event/delete/{id}");

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> UpdateBalanceAsync(Guid transactionId)
    {
        var request = Request
            .Post("/accounting-balance/update")
            .WithAdditionalQueryParameter("transactionId", transactionId);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }
    
    public async Task<OperationResult<bool>> UpdateMaxPriceAsync(Guid transactionId)
    {
        var request = Request
            .Post("/analytics/update-max-price")
            .WithAdditionalQueryParameter("transactionId", transactionId);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<List<AccountBalance>>> GetAllForDateAsync(DateTime? dateTime = null)
    {
        var request = Request
            .Post("/accounting-balance/all-for-date")
            .WithAdditionalQueryParameter("dateTime", dateTime);

        var operationResult = await SendRequestAsync<List<AccountBalance>>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> RenewBalanceAsync(Guid accountId, DateTime dateTime)
    {
        var request = Request
            .Post("/accounting-balance/renew")
            .WithAdditionalQueryParameter("accountId", accountId)
            .WithAdditionalQueryParameter("dateTime", dateTime);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> CreateDayResultTransactionAsync(Guid accountId, DateTime dateTime)
    {
        var request = Request
            .Post("/accounting-balance/create-day-result")
            .WithAdditionalQueryParameter("accountId", accountId)
            .WithAdditionalQueryParameter("dateTime", dateTime);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }

    public async Task<OperationResult<bool>> SendEmailAsync(Guid transactionId)
    {
        var request = Request
            .Post("/accounting-balance/send-report")
            .WithAdditionalQueryParameter("transactionId", transactionId);

        var operationResult = await SendRequestAsync<bool>(request);
        if (operationResult.IsSuccessful)
        {
            return operationResult;
        }
        return operationResult;
    }
}