using OpenAI.Models.Models;

namespace OpenAI.App.Handlers
{
    public interface IOpenAIResponseHandler
    {
        string HandleResponse(ResponseData responseData);
    }
}
