using OpenAI.Models.Models;

namespace OpenAI.App.Handlers
{
    public class OpenAIResponseHandler : IOpenAIResponseHandler
    {
        public string HandleResponse(ResponseData responseData)
        {
            ArgumentNullException.ThrowIfNull(responseData, nameof(responseData));

            var responseContent = responseData?.Choices?.FirstOrDefault()?.Message?.Content;

            ArgumentNullException.ThrowIfNullOrEmpty(responseContent, nameof(responseContent));

            return responseContent;
        }
    }
}
