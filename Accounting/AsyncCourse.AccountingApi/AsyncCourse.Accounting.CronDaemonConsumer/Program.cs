using AsyncCourse.Accounting.CronDaemonConsumer;

var cronJobHanler = new AccountBalanceHanler();

while (true)
{
    var cancellationToken = new CancellationToken();

    await cronJobHanler.ProcessStreamEvent(cancellationToken);
    await cronJobHanler.ProcessBusinessEvent(cancellationToken);
}