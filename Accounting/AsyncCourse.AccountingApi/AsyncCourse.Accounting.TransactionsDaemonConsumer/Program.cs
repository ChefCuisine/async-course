using AsyncCourse.Accounting.TransactionsDaemonConsumer;

var transactionsHanler = new TransactionsHanler();

while (true)
{
    var cancellationToken = new CancellationToken();

    await transactionsHanler.ProcessStreamEvent(cancellationToken);
    await transactionsHanler.ProcessBusinessEvent(cancellationToken);
}