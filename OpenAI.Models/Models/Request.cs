using System.Collections.Generic;
using System.Text.Json.Serialization;

namespace OpenAI.Models.Models
{
    public class Request
    {
        [JsonPropertyName("model")]
        public string ModelId { get; set; } = "text-davinci-003";

        [JsonPropertyName("messages")]
        public List<Message> Messages { get; set; }
    }
}
