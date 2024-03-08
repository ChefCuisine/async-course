using Confluent.Kafka;
using Newtonsoft.Json;

namespace AsyncCourse.Template.Kafka.MessageBus;

public interface ITemlateKafkaMessageBus
{
    Task<DeliveryResult<Null, string>> SendMessageAsync(string topic, string message);
    T Consume<T>(string topic, CancellationToken cancellationToken);
}

public class TemlateKafkaMessageBus : ITemlateKafkaMessageBus
{
    // todo вероятнее всего можно отправлять в кафку батчем, нужно узнать как и переписать метод чтоб принимал массив
    public async Task<DeliveryResult<Null, string>> SendMessageAsync(string topic, string message)
    {
        try
        {
            var producerConfig = new ProducerConfig
            {
                BootstrapServers = Constants.BootstrapServer,
                // ClientId = Dns.GetHostName()
            };
            using var producer = new ProducerBuilder<Null, string>(producerConfig).Build();
            var result = await producer.ProduceAsync(topic, new Message<Null, string>
            {
                Value = message
            });

            return result;
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
            var consumerConfig = new ConsumerConfig
            {
                GroupId = Constants.CustomGroup, // Указать обязательно. Определяет какой группе принадлежит консьюмер
                BootstrapServers = Constants.BootstrapServer,
                AutoOffsetReset = AutoOffsetReset.Earliest
            };

            using var consumer = new ConsumerBuilder<Ignore, string>(consumerConfig).Build();
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
        catch (Exception e)
        {
            Console.WriteLine(e);
            throw;
        }

        return default;
    }
}