using UserService2;
using UserService2.Kafka;
using UserService2.Services;

var builder = Host.CreateApplicationBuilder(args);

builder.Services.AddSingleton<KafkaProducer>();

builder.Services.AddSingleton<UserManagementService>();

builder.Services.AddHostedService<KafkaConsumer>();

builder.Services.AddHostedService<Worker>();

var host = builder.Build();

host.Run();