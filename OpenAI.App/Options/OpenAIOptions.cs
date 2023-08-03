namespace OpenAI.App.Options
{
    public class OpenAIOptions
    {
        public const string OpenAISection = "OpenAISettings";

        public string ApiUrl { get; set; }
        public string ApiKey { get; set; }

        public string ApiUrlCompletion { get => $"{ApiUrl}/completions"; }
    }
}
