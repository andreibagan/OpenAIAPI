using Microsoft.AspNetCore.Mvc;
using OpenAI.App.Handlers;
using OpenAI.App.Services;

namespace OpenAI.App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class OpenAIController : ControllerBase
    {
        private readonly IOpenAIService _openAIService;
        private readonly IOpenAIResponseHandler _responseHandler;

        public OpenAIController(IOpenAIService openAIService, IOpenAIResponseHandler responseHandler)
        {
            _openAIService = openAIService;
            _responseHandler = responseHandler;
        }

        [HttpPost("SendMessage")]
        public async Task<IActionResult> SendMessageAsync([FromBody] string message)
        {
            var responseData = await _openAIService.SendMessageAsync(message);
            return Ok(_responseHandler.HandleResponse(responseData));
        }
    }
}