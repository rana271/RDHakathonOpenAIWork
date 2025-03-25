using Newtonsoft.Json;

namespace Hakaton.Services
{
    public class PromptService : IPromptService
    {
        public async Task<RootPrompt> GetPromptFromFileAsync(string filePath)
        {
            // Read the JSON file
            var jsonContent = await File.ReadAllTextAsync(filePath);

            // Deserialize the JSON into a PromptModel object
            var promptModel = JsonConvert.DeserializeObject<RootPrompt>(jsonContent);

            return promptModel;
        }
    }
}
