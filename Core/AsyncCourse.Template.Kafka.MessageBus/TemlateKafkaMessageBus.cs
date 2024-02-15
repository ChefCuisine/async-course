using Confluent.Kafka;
using Newtonsoft.Json;

namespace AsyncCourse.Template.Kafka.MessageBus;

public interface ITemlateKafkaMessageBus
{
    Task<DeliveryResult<Null, string>> SendMessageAsync(string topic, string message);
    T Consume<T>(string topic, CancellationToken cancellationToken);
}

public class TemlateKafkaMessageBus : IDisposable, ITemlateKafkaMessageBus
{
    private readonly IProducer<Null, string> producer;
    private readonly IConsumer<Ignore, string> consumer;

    public TemlateKafkaMessageBus() : this(Constants.BootstrapServer)
    {
    }
    
    public TemlateKafkaMessageBus(string host)
    {
        var producerConfig = new ProducerConfig
        {
            BootstrapServers = host,
            // ClientId = Dns.GetHostName()
        };
        var consumerConfig = new ConsumerConfig
        {
            GroupId = Constants.CustomGroup, // Указать обязательно. Определяет какой группе принадлежит консьюмер
            BootstrapServers = host,
            AutoOffsetReset = AutoOffsetReset.Earliest
        };

        producer = new ProducerBuilder<Null, string>(producerConfig).Build();
        consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
    }
    
    public async Task<DeliveryResult<Null, string>> SendMessageAsync(string topic, string message)
    {
        try
        {
            using (producer)
            {
                var result = await producer.ProduceAsync(topic, new Message <Null, string>
                {
                    Value = message
                });

                return result;
            }
        }
        catch (Exception e)
        {
            Console.WriteLine($"Error during sending message: {e.Message}");
            throw;
        }
    }

    public T Consume<T>(string topic, CancellationToken cancellationToken)
    {
        try
        {
            using (consumer)
            {
                consumer.Subscribe(topic);

                try
                {
                    while (!cancellationToken.IsCancellationRequested)
                    {
                        var consumerResult = consumer.Consume(cancellationToken);
                        if (consumerResult.Message == null)
                        {
                            Console.WriteLine("ConsumerResult.Message is null");
                            break;
                        }

                        var deserialized = JsonConvert.DeserializeObject<T>(consumerResult.Message.Value);
                        if (deserialized == null)
                        {
                            Console.WriteLine("Deserialized ConsumerResult.Message is null");
                            break;
                        }

                        Console.WriteLine($"Delivery Timestamp: {consumerResult.Message.Timestamp.UtcDateTime}");
                        return deserialized;
                    }
                }
                catch (OperationCanceledException e)
                {
                    Console.WriteLine(e.Message);
                    consumer.Close();
                }
            }
        }
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return default;
    }

    public void Dispose()
    {
        producer.Dispose();
        consumer.Dispose();
    }
}