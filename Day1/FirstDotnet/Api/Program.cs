var builder = WebApplication.CreateBuilder(args);
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddControllers();

builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});

//builder.Services.AddSingleton<IOutputCacheStore, YVJsonFileCacheStore>();
builder.Services.AddOutputCache(); /// save output IN-MEMORY

var app = builder.Build();

app.UseCors(); // listen to method options only
app.UseOutputCache();
app.UseStaticFiles();
app.MapControllers();
app.Run();


