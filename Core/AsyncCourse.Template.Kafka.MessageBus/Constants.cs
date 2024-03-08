namespace AsyncCourse.Template.Kafka.MessageBus;

public static class Constants
{
    public const string BootstrapServer = "localhost:9092";

    public const string TestTopic = "test";

    public const string CustomGroup = "custom-group";
    
    public const string AccountsStreamTopic = "accounts-stream"; // топик для CUD-событий по аккаунтам
    public const string AccountsTopic = "accounts"; // топик для BE-событий по аккаунтам

    public const string IssuesStreamTopic = "issues-stream";
    public const string IssuesTopic = "issues";
}