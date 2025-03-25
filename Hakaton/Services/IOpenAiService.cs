namespace Hakaton.Services
{
    public interface IOpenAiService
    {
          Task<string> GetOpenAiResponseAsync(string prompt);
    }
}
