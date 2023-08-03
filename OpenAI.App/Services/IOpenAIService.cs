using OpenAI.Models.Models;

namespace OpenAI.App.Services
{
    public interface IOpenAIService
    {
        Task<ResponseData> SendMessageAsync(string message);
    }
}
