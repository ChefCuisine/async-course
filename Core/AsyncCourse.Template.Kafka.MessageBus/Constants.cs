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
    
    public const string TransactionsStreamTopic = "transactions-stream";
    public const string TransactionsTopic = "transactions";
    
    public const string AccountBalanceStreamTopic = "account-balances-stream";
    public const string AccountBalanceTopic = "account-balances";

    public static class Producers
    {
        public const string AccountsStream = "AccountsStreamProducer";
        public const string AccountsBusiness = "AccountsBusinessProducer";
        public const string IssuesStream = "IssuesStreamProducer";
        public const string IssuesBusiness = "IssuesBusinessProducer";
        public const string TransactionsStream = "TransactionsStreamProducer";
        public const string TransactionsBusiness = "TransactionsBusinessProducer";
        public const string AccountBalanceStream = "AccountBalanceStreamProducer";
        public const string AccountBalanceBusiness = "AccountBalanceBusinessProducer";
    }
}