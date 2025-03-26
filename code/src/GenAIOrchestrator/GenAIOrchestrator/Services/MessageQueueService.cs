using RabbitMQ.Client;
using RabbitMQ.Client.Exceptions;
using System.Text;

namespace GenAIOrchestrator.Services
{
    public class MessageQueueService : IMessageQueueService, IDisposable
    {
        private readonly string _hostname;
        private readonly string _queueName;
        private IConnection _connection;
        private IModel _channel;

        public MessageQueueService(string hostname, string queueName)
        {
            _hostname = hostname;
            _queueName = queueName;
            InitializeConnection();
        }

        private void InitializeConnection()
        {
            try
            {
                var factory = new ConnectionFactory { HostName = _hostname };
                _connection = factory.CreateConnection();
                _channel = _connection.CreateModel();
                _channel.QueueDeclare(queue: _queueName, durable: true, exclusive: false, autoDelete: false, arguments: null);
            }
            catch (BrokerUnreachableException ex)
            {
                Console.WriteLine($"[RabbitMQ Error] Could not connect: {ex.Message}");
            }
        }

        public Task PublishMessageAsync(string topic, string message)
        {
            if (_channel == null)
            {
                Console.WriteLine("RabbitMQ is not available. Skipping message publish.");
                return Task.CompletedTask;
            }

            var body = Encoding.UTF8.GetBytes(message);
            _channel.BasicPublish(exchange: "", routingKey: _queueName, basicProperties: null, body: body);
            return Task.CompletedTask;
        }

        public void Dispose()
        {
            _channel?.Close();
            _connection?.Close();
        }
    }
}
