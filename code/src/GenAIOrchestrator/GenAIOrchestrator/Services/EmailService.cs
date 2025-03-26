using iText.Kernel.Pdf.Canvas.Parser;
using iText.Kernel.Pdf;

namespace GenAIOrchestrator.Services
{
    public class EmailService: IEmailService
    {
        private readonly IAIService _aiProcessor;
        private readonly IMessageQueueService _messageQueueService;

        public EmailService(IAIService aiProcessor, IMessageQueueService messageQueueService)
        {
            _aiProcessor = aiProcessor;
            _messageQueueService = messageQueueService;
        }

        public async Task<string> ProcessEmailAsync(IFormFile pdfFile)
        {
            string extractedText = await ExtractTextFromPdfAsync(pdfFile);
            string classification = await _aiProcessor.ClassifyEmailAsync(extractedText);
            await _messageQueueService.PublishMessageAsync("email_classification", classification);
            return classification;
        }

        private async Task<string> ExtractTextFromPdfAsync(IFormFile pdfFile)
        {
            using (var stream = pdfFile.OpenReadStream())
            using (var pdfReader = new PdfReader(stream))
            using (var pdfDoc = new PdfDocument(pdfReader))
            {
                var text = "";
                for (int i = 1; i <= pdfDoc.GetNumberOfPages(); i++)
                {
                    text += PdfTextExtractor.GetTextFromPage(pdfDoc.GetPage(i));
                }
                return text;
            }
        }
    }
}
