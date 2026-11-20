using Confluent.Kafka;
using System;
using System.Collections.Generic;
using System.Text;

namespace UserService2.Kafka
{
    public class KafkaConsumer : BackgroundService
    {
        protected override Task ExecuteAsync(
            CancellationToken stoppingToken)
        {
            return Task.Run(() =>
            {
                var config = new ConsumerConfig
                {
                    BootstrapServers = "10.9.2.80:9092",
                    GroupId = "user-group-2",
                    AutoOffsetReset = AutoOffsetReset.Earliest,
                    EnableAutoCommit = false
                };

                using var consumer =
                    new ConsumerBuilder<string, string>(config).Build();

                consumer.Subscribe("order.created");

                Console.WriteLine(
                    "Ожидание сообщений из order.created"
                );

                try
                {
                    while (!stoppingToken.IsCancellationRequested)
                    {
                        var result = consumer.Consume(stoppingToken);

                        Console.WriteLine("Получен заказ:");
                        Console.WriteLine(result.Message.Value);

                        consumer.Commit(result);
                    }
                }
                catch (OperationCanceledException)
                {
                    Console.WriteLine("Consumer остановлен");
                }
                finally
                {
                    consumer.Close();
                }
            }, stoppingToken);
        }
    }
}
