namespace AsyncCourse.Template.Kafka.MessageBus;

public static class Constants
{
    public const string BootstrapServer = "localhost:9092";

    public const string TestTopic = "test";

    public const string CustomGroup = "custom-group";
    
    // Топики с именем "{domain-model}-stream" - топики для CUD-событий по {domain-model}
    // Топики с именем "{domain-model}" - топики для BE-событий по {domain-model}
    
    public const string AccountsStreamTopic = "accounts-stream";
    public const string AccountsTopic = "accounts";

    public const string IssuesStreamTopic = "issues-stream";
    public const string IssuesTopic = "issues";
}