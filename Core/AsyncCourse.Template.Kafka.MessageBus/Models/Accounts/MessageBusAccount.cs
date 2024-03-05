namespace AsyncCourse.Template.Kafka.MessageBus.Models.Accounts;

public class MessageBusAccount
{
    public Guid Id { get; set; }
    public string Email { get; set; }
    public string Name { get; set; }
    public string Surname { get; set; }
    public string Role { get; set; }
}