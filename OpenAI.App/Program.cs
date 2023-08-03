using OpenAI.App.Handlers;
using OpenAI.App.Middlewares;
using OpenAI.App.Options;
using OpenAI.App.Services;
using OpenAI.Utility.HttpUtility;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddOptions<OpenAIOptions>()
    .BindConfiguration(OpenAIOptions.OpenAISection)
    .Validate(x => !string.IsNullOrWhiteSpace(x.ApiUrl) && !string.IsNullOrWhiteSpace(x.ApiKey))
    .ValidateOnStart();

builder.Services.AddHttpClient<HttpRequestUtility>();
builder.Services.AddScoped<IOpenAIService, OpenAIService>();
builder.Services.AddScoped<IOpenAIResponseHandler, OpenAIResponseHandler>();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

//app.UseHttpsRedirection();
//app.UseAuthorization();
app.UseMiddleware<ExceptionMiddleware>();

app.MapControllers();
app.Run();
