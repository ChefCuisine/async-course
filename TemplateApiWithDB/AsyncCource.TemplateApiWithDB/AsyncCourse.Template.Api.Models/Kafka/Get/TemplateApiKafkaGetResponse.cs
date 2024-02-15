namespace AsyncCourse.Template.Api.Domain.Models.Kafka.Get;

public class TemplateApiKafkaGetResponse
{
    public int OrderId { get; set; }
    public int ProductId { get; set; }
    public int CustomerId { get; set; }
    public int Quantity { get; set; }
    public string Status { get; set; }
}