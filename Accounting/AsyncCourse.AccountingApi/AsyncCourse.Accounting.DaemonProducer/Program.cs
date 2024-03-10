using AsyncCourse.Accounting.DaemonProducer;

var transactionHandler = new TransactionHandler();

while (true)
{
    await transactionHandler.ProduceEvent();
}