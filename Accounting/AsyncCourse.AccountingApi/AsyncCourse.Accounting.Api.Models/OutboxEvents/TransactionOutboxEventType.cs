namespace AsyncCourse.Accounting.Api.Models.OutboxEvents;

public enum TransactionOutboxEventType
{
    Unknown = 0,
    AddMoney = 1,
    RemoveMoney = 2,
}