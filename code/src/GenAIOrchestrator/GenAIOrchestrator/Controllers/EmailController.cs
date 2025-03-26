// GenAIOrchestrator.Api - Main API
using Microsoft.AspNetCore.Mvc;
using GenAIOrchestrator.Services;

[ApiController]
[Route("api/email")]
public class EmailController : ControllerBase
{
    private readonly IEmailService _emailProcessingService;

    public EmailController(IEmailService emailProcessingService)
    {
        _emailProcessingService = emailProcessingService;
    }

    [HttpPost("process")]
    public async Task<IActionResult> ProcessEmail([FromForm] IFormFile pdfFile)
    {
        if (pdfFile == null || pdfFile.Length == 0)
            return BadRequest("Invalid file.");

        var result = await _emailProcessingService.ProcessEmailAsync(pdfFile);
        return Ok(result);
    }
}
