using AsyncCourse.Accounting.IssuesDaemonConsumer;

var issuesHandler = new IssuesHanler();

while (true)
{
    var cancellationToken = new CancellationToken();

    await issuesHandler.ProcessStreamEvent(cancellationToken);
    await issuesHandler.ProcessBusinessEvent(cancellationToken);
}