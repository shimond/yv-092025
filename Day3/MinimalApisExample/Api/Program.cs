var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<YvDataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("yvDatabase")));
builder.Services.AddScoped<IProductsRepository, EFProductsRepository>(); 

builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();

await app.Services.CreateScope().ServiceProvider.GetRequiredService<YvDataContext>().Database.EnsureCreatedAsync();

app.MapProductsApis();
app.MapConfigurationApis();

app.Run();


