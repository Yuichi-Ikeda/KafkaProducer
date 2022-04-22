using Confluent.Kafka;
using Newtonsoft.Json;
using Microsoft.Extensions.Configuration;

namespace KafkaProducer
{
    class KafkaMessage
    {
        public long id { get; set; }
        public string? timestamp { get; set; }

        public string? message { get; set; }
    }

    class Producer
    {
        static void Main(string[] args)
        {
            if (args.Length != 1)
            {
                Console.WriteLine("Please provide the configuration file path as a command line argument");
            }

            IConfiguration configuration = new ConfigurationBuilder()
                .AddIniFile(args[0])
                .Build();

            const string topic = "users";

            using (var producer = new ProducerBuilder<string, string>(
                configuration.AsEnumerable()).Build())
            {
                var numProduced = 0;
                const int numMessages = 2;
                for (int i = 0; i < numMessages; ++i)
                {
                    var item = JsonConvert.SerializeObject(new KafkaMessage
                    {
                        id = i,
                        timestamp = DateTime.UtcNow.ToString("yyyy/MM/dd HH:mm;ss.fff"),
                        message = "This is a sample message. Something in here 0123456789012345678."
                    });

                    producer.Produce(topic, new Message<string, string> { Key = i.ToString(), Value = item },
                        (deliveryReport) =>
                        {
                            if (deliveryReport.Error.Code != ErrorCode.NoError)
                            {
                                Console.WriteLine($"Failed to deliver message: {deliveryReport.Error.Reason}");
                            }
                            else
                            {
                                Console.WriteLine($"Produced event to topic {topic}: value = {item}");
                                numProduced += 1;
                            }
                        });
                }

                producer.Flush(TimeSpan.FromSeconds(10));
                Console.WriteLine($"{numProduced} messages were produced to topic {topic}");
            }
        }
    }
}