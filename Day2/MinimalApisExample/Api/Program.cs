var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<IProductsRepository, InMemoryProductsRepository>(); 

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

app.MapProductsApis();

//same as:
//ProductsApis.MapProductsApis(app);

app.Run();


