using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;
using System.Text.Json;
using UserService2.Models;

namespace UserService2.Kafka
{
    public class KafkaProducer : IDisposable
    {
        private readonly IProducer<string, string> _producer;

        public KafkaProducer()
        {
            var config = new ProducerConfig
            {
                BootstrapServers = "10.9.2.80:9092"
            };

            _producer = new ProducerBuilder<string, string>(
                config
            ).Build();
        }

        public async Task RegisterUser(User user)
        {
            string json = JsonSerializer.Serialize(user);

            var result = await _producer.ProduceAsync(
                "user.registered",
                new Message<string, string>
                {
                    Key = user.Id.ToString(),
                    Value = json
                });

            Console.WriteLine(
                $"Отправлено в Kafka: {result.TopicPartitionOffset}"
            );
        }

        public void Dispose()
        {
            _producer.Dispose();
        }
    }
}
