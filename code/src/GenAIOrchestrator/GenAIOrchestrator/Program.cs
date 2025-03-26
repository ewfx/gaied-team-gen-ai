using GenAIOrchestrator.Services;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

// Add services to the container.
// Read OpenAI API Key

// Read RabbitMQ settings
var rabbitMqHostName = configuration["RabbitMQ:HostName"] ?? "localhost";
var rabbitMqQueueName = configuration["RabbitMQ:QueueName"] ?? "email_classification";

// Register Dependencies
builder.Services.AddSingleton<IEmailService, EmailService>();
builder.Services.AddSingleton<IAIService>(sp => new AIService());
builder.Services.AddSingleton<IMessageQueueService>(sp => new MessageQueueService(rabbitMqHostName, rabbitMqQueueName));


builder.Services.AddControllers();


var app = builder.Build();

// Configure the HTTP request pipeline.


app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
