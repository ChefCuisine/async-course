using AsyncCourse.Issues.DaemonProducer;

var issuesHandler = new IssuesHandler();

while (true)
{
    await issuesHandler.ProduceEvent();
}