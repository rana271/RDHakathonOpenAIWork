
using Hakaton.Model;
//using Newtonsoft.Json;
using System.Text;
using System.Text.Json;
namespace Hakaton.Services
{
    public class OpenAiService : IOpenAiService
    {
        private readonly HttpClient _httpClient;
        private readonly string _apiKey;
        public OpenAiService(HttpClient httpClient)
        {
            _httpClient = httpClient;
            //_apiKey = "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"; // Replace this with your OpenAI API key
            _apiKey = Environment.GetEnvironmentVariable("APIKey");

        }
        /// <summary>
        /// Service for OpenAI API
        /// </summary>
        /// <param name="prompt"></param>
        /// <returns></returns>
        public async Task<string> GetOpenAiResponseAsync(string prompt)
        {
            var requestBody = new
            {
                model = "gpt-4o-mini",
                max_tokens = 50,
                messages = new[]
                {
                    new { role = "system", content = "You are a genealogy assistant for GlobalRoots. Generate a location-specific family query." },
                    new { role = "user", content = prompt }
                }
            };
            var content = new StringContent(JsonSerializer.Serialize(requestBody), Encoding.UTF8, "application/json");
            _httpClient.DefaultRequestHeaders.Add("Authorization", $"Bearer {_apiKey}");
            var response = await _httpClient.PostAsync(Environment.GetEnvironmentVariable("OpenAPiEndpoint"), content);
            response.EnsureSuccessStatusCode();

           
            if (response.IsSuccessStatusCode)
            {
                var responseJson = await response.Content.ReadAsStringAsync();
                var result = JsonSerializer.Deserialize<PromptResponse>(responseJson);
                return responseJson;
            }
            else
            {
                return $"Error: {response.ReasonPhrase}";
            }
            
        }
    }
    public class OpenAiResponse
    {
        public OpenAiChoice[] Choices { get; set; }
    }

    public class OpenAiChoice
    {
        public OpenAiMessage Message { get; set; }
    }

    public class OpenAiMessage
    {
        public string Content { get; set; }
    }
}
