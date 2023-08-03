using System.Text;
using System.Text.Json;

namespace OpenAI.App.Helpers
{
    public static class HttpContextHelper
    {
        public static string GetUrlFromContext(this HttpContext httpContext)
        {
            return $"{httpContext.Request.Scheme}://{httpContext.Request.Host}{httpContext.Request.Path.Value}{(httpContext.Request.QueryString.HasValue ?
                ($"?{httpContext.Request.QueryString.Value}") :
                string.Empty)}";
        }

        public static string GetHeadersFromContext(this HttpContext httpContext)
        {
            return JsonSerializer.Serialize(httpContext.Request.Headers
                .Where(n => n.Value.Count > 0)
                .Select(n => new
                {
                    n.Key,
                    Value = n.Value.ToString()
                }));
        }

        public static string GetBodyFromContext(this HttpContext httpContext)
        {
            var body = string.Empty;

            if (httpContext.Request.Body != null && httpContext.Request.Body.Position > 0)
            {
                httpContext.Request.Body.Position = 0;
                using (var reader = new StreamReader(httpContext.Request.Body, Encoding.UTF8))
                {
                    body = reader.ReadToEnd();
                }
            }

            return body;
        }
    }
}
