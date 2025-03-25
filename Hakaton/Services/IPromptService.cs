namespace Hakaton.Services
{
    public interface IPromptService
    {
        Task<RootPrompt> GetPromptFromFileAsync(string filePath);
    }
}
