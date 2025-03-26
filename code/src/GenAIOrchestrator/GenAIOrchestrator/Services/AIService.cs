using System.Text;
using System.Text.Json;
using GenAIOrchestrator.Services;

public class AIService : IAIService
{
    private readonly HttpClient _httpClient;

    public AIService()
    {
        _httpClient = new HttpClient();
    }

    public async Task<string> ClassifyEmailAsync(string emailText)
    {
        var requestBody = new
        {
            model = "llama3",
            prompt = emailText,
            max_tokens = 50,
            temperature = 0.3
        };

        var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
        var response = await _httpClient.PostAsync("http://localhost:4891/v1/completions", content);

        if (!response.IsSuccessStatusCode)
        {
            return "Error in AI Processing";
        }

        var responseString = await response.Content.ReadAsStringAsync();
        var jsonResponse = JsonSerializer.Deserialize<JsonElement>(responseString);
        return jsonResponse.GetProperty("choices")[0].GetProperty("text").GetString().Trim();
    }
}
