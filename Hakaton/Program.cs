using Hakaton.Services;
using static System.Runtime.InteropServices.JavaScript.JSType;
using System.Runtime;
public class Program
{
    public static void Main(string[] args)
    {
        var builder = WebApplication.CreateBuilder(args);

        // Add services to the container.

        builder.Services.AddControllers();
        //builder.Services.AddSingleton<Data.Neo4jContext>();

        //builder.Services.Configure<AiSettings>(builder.Configuration.GetSection("Ai"));
        //var vendor = builder.Configuration["AiVendor"]?.ToLower() ?? "mock";
        builder.Services.AddHttpClient<IOpenAiService,OpenAiService>();
        builder.Services.AddTransient<IPromptService,PromptService>();
        
        // Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
        builder.Services.AddEndpointsApiExplorer();
        builder.Services.AddSwaggerGen();

        var app = builder.Build();

        // Configure the HTTP request pipeline.
        if (app.Environment.IsDevelopment())
        {
            app.UseSwagger();
            app.UseSwaggerUI();
        }

        app.UseHttpsRedirection();

        app.UseAuthorization();


        app.MapControllers();

        app.Run();
    }
}
//var builder = WebApplication.CreateBuilder(args);

//// Register OpenAiService for dependency injection
//builder.Services.AddHttpClient<OpenAiService>();

//var app = builder.Build();

//app.MapGet("/", () => "Hello World!");

//app.MapPost("/get-openai-response", async (OpenAiService openAiService, string prompt) =>
//{
//    var response = await openAiService.GetOpenAiResponseAsync(prompt);
//    return Results.Ok(response);
//});

//app.Run();
