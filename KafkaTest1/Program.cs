using Confluent.Kafka;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var producerConfig = new ProducerConfig();
builder.Configuration.Bind("producer", producerConfig);
builder.Services.AddSingleton<ProducerConfig>(producerConfig);

builder.Services.AddControllers();

var app = builder.Build();

// Configure the HTTP request pipeline.

app.UseAuthorization();

app.MapControllers();

app.Run();
