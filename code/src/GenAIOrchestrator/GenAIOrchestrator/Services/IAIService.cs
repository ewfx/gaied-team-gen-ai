namespace GenAIOrchestrator.Services
{
    public interface IAIService
    {
        Task<string> ClassifyEmailAsync(string emailText);
    }
}
