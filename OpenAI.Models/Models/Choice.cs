﻿using System.Text.Json.Serialization;

namespace OpenAI.Models.Models
{
    public class Choice
    {
        [JsonPropertyName("index")]
        public int Index { get; set; }

        [JsonPropertyName("message")]
        public Message Message { get; set; } = new Message();

        [JsonPropertyName("finish_reason")]
        public string FinishReason { get; set; }
    }
}
