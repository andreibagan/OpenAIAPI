using Microsoft.Extensions.Options;
using OpenAI.App.Options;
using OpenAI.Models.Models;
using OpenAI.Utility.HttpUtility;
using System.Text.Json;

namespace OpenAI.App.Services
{
    public class OpenAIService : IOpenAIService
    {
        private readonly HttpRequestUtility _httpRequestUtility;
        private readonly OpenAIOptions _openAIOptions;

        public OpenAIService(HttpRequestUtility httpRequestUtility, IOptions<OpenAIOptions> openAIOptions)
        {
            _httpRequestUtility = httpRequestUtility;
            _openAIOptions = openAIOptions.Value;
        }

        public async Task<ResponseData> SendMessageAsync(string message)
        {
            ArgumentNullException.ThrowIfNullOrEmpty(message, nameof(message));

            var request = GetRequest(message);

            var response = await _httpRequestUtility.SendHttpRequest(_openAIOptions.ApiUrlCompletion, 
                HttpMethod.Post, 
                request, 
                h => h.Add("Authorization", $"Bearer {_openAIOptions.ApiKey}"));

            ArgumentNullException.ThrowIfNullOrEmpty(response, nameof(response));

            return JsonSerializer.Deserialize<ResponseData>(response);
        }

        private Request GetRequest(string message)
        {
            return new Request
            {
                Messages = new List<Message>
                {
                    new Message
                    {
                        Content = message
                    }
                }
            };
        }
    }
}
