using Api.Contracts;
using Microsoft.Extensions.DependencyInjection;

var builder = WebApplication.CreateBuilder(args);

if (builder.Environment.IsDevelopment())
{
    //  כמה אובייקטים לייצר
    //builder.Services.AddSingleton<IProductsRepository, InMemoryProductsRepository>(); // one for the entire application lifetime
    builder.Services.AddScoped<IProductsRepository, InMemoryProductsRepository>(); // one per request
    //builder.Services.AddTransient<IProductsRepository, InMemoryProductsRepository>(); // each time requested by function or dependency injection ctor

    //builder.Services.AddKeyedScoped<IProductsRepository, InMemoryProductsRepository>("TheKey");
    //builder.Services.AddKeyedTransient<IProductsRepository, InMemoryProductsRepository>("TheKey");
    //builder.Services.AddKeyedSingleton<IProductsRepository, InMemoryProductsRepository>("TheKey");
}
else
{
    //builder.Services.AddScoped<IProductsRepository, RealFromDbProductRepository>();
}


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

builder.Services.AddOutputCache();

var app = builder.Build();
//app.UseLoggingMiddleware();
app.MapControllers();
app.Run();


