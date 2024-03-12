using AsyncCourse.Accounting.IssuesDaemon;

var issuesHandler = new IssuesHanler();

while (true)
{
    var cancellationToken = new CancellationToken();

    await issuesHandler.ProcessStreamEvent(cancellationToken);
    await issuesHandler.ProcessBusinessEvent(cancellationToken);
}