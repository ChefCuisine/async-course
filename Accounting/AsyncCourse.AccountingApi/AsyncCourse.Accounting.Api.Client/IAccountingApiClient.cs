﻿using AsyncCourse.Accounting.Api.Models.Accounts;
using AsyncCourse.Accounting.Api.Models.Balances;
using AsyncCourse.Accounting.Api.Models.Issues;
using AsyncCourse.Accounting.Api.Models.OutboxEvents;
using AsyncCourse.Client;

namespace AsyncCourse.Accounting.Api.Client;

public interface IAccountingApiClient // todo поменять bool на вменяемый ответ
{
    Task<OperationResult<bool>> SaveAccountAsync(AccountingAccount account);
    Task<OperationResult<bool>> UpdateAccountAsync(AccountingAccount account);
    Task<OperationResult<bool>> SaveIssueAsync(AccountingIssue issue);
    Task<OperationResult<bool>> ReassignIssueAsync(AccountingBusinessChangedIssue issue);
    Task<OperationResult<bool>> CloseIssueAsync(AccountingBusinessChangedIssue issue);
    Task<OperationResult<TransactionOutboxEvent>> ReadTransactionEventAsync();
    Task<OperationResult<bool>> DeleteTransactionEventAsync(Guid id);
    Task<OperationResult<bool>> UpdateBalanceAsync(Guid transactionId);
    Task<OperationResult<bool>> UpdateMaxPriceAsync(Guid transactionId);
    Task<OperationResult<List<AccountBalance>>> GetAllForDateAsync(DateTime? dateTime = null);
    Task<OperationResult<bool>> RenewBalanceAsync(Guid accountId, DateTime dateTime);
    Task<OperationResult<bool>> CreateDayResultTransactionAsync(Guid accountId, DateTime dateTime);
    Task<OperationResult<bool>> SendEmailAsync(Guid transactionId);
}