namespace AsyncCourse.Template.Kafka.MessageBus;

public static class Constants
{
    public const string BootstrapServer = "localhost:9092";

    public const string TestTopic = "test";

    public const string CustomGroup = "custom-group";
    
    public const string AccountCreateTopic = "account-create";
    public const string AccountUpdateTopic = "account-update";
    
    public const string IssueCreateTopic = "issue-create";
    public const string IssueUpdateTopic = "issue-update";
}