using AsyncCourse.Issues.AccountsDaemonConsumer;

var accountsHandler = new AccountsHanler();

while (true)
{
    var cancellationToken = new CancellationToken();

    await accountsHandler.ProcessStreamEvent(cancellationToken);
    await accountsHandler.ProcessBusinessEvent(cancellationToken);
}