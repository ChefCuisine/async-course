using AsyncCourse.Accounting.DaemonProducer;

var transactionHandler = new TransactionsHandler();

while (true)
{
    await transactionHandler.ProduceEvent();
}