namespace GenAIOrchestrator.Services
{
    public interface IMessageQueueService
    {
        Task PublishMessageAsync(string topic, string message);
    }
}
