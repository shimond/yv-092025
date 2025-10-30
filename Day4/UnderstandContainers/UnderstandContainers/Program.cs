using System.Data;
using Microsoft.Extensions.DependencyInjection;
using Seq.Extensions.Logging;

var builder = WebApplication.CreateBuilder(args);
if (builder.Environment.IsDevelopment())
{
    builder.Logging.AddSeq();
}

builder.Services.AddOpenApi();

var app = builder.Build();

app.MapOpenApi();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapGet("api/os", () =>
{
    app.Logger.LogInformation("I AM RUNNING AS A CONTAINER . THE OS endpoint was called {environment}", Environment.OSVersion);
    return Results.Ok(Environment.OSVersion);
});


app.MapGet("/api/createFile/{fileName}/{content}", (string fileName, string content) =>
{
    File.WriteAllText($"{fileName}", content);
    app.Logger.LogTrace("File '{fileName}' created with content: {content}", fileName, content);
    return Results.Ok($"File '{fileName}' created with content: {content}");
});

app.MapGet("/api/readFile/{fileName}", (string fileName) =>
{
    var filePath = $"{fileName}";
    if (!File.Exists(filePath))
    {
        app.Logger.LogWarning("File '{fileName}' not found", fileName);
        return Results.NotFound($"File '{fileName}' not found.");
    }
    var content = File.ReadAllText(filePath);
    return Results.Ok(content);
});

app.Run();
