namespace GenAIOrchestrator.Services
{
    public interface IEmailService
    {
        Task<string> ProcessEmailAsync(IFormFile pdfFile);
    }
}
