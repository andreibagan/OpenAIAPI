using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace OpenAI.Utility.HttpUtility
{
    public class HttpRequestUtility
    {
        private readonly HttpClient _httpClient;

        public HttpRequestUtility(HttpClient httpClient)
        {
            _httpClient = httpClient;
        }

        public async Task<string> SendHttpRequest<T>(string url, HttpMethod httpMethod, T model = null, Action<HttpRequestHeaders> headersHandler = null) where T : class
        {
            using (var requestMessage = new HttpRequestMessage(httpMethod, url))
            {
                var productValue = new ProductInfoHeaderValue("OpenAIAPI", "1.0");
                var commentValue = new ProductInfoHeaderValue($"(+url)");

                requestMessage.Headers.UserAgent.Add(productValue);
                requestMessage.Headers.UserAgent.Add(commentValue);
                headersHandler?.Invoke(requestMessage.Headers);

                if (httpMethod != HttpMethod.Get && model != null)
                {
                    var stringContent = new StringContent(JsonSerializer.Serialize(model), Encoding.UTF8, "application/json");
                    requestMessage.Content = stringContent;
                }

                using (requestMessage.Content)
                {
                    using (var response = await _httpClient.SendAsync(requestMessage))
                    {
                        using (var content = response.Content)
                        {
                            var responseBody = await content.ReadAsStringAsync();
                            if (!response.IsSuccessStatusCode)
                            {
                                throw new HttpRequestException($"Error with a post request to {url}. StatusCode: {response.StatusCode}. ReasonPhrase: {response.ReasonPhrase}. ResponseBody: {responseBody}");
                            }

                            return responseBody;
                        }
                    }
                }
            }
        }
    }
}
