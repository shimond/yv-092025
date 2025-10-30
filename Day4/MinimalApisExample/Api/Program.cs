var builder = WebApplication.CreateBuilder(args);

builder.Services.Configure<YVCollectionConfig>(builder.Configuration.GetSection("YVCollection"));

builder.Services.AddServiceDiscovery();
builder.Services.ConfigureHttpClientDefaults(static http => // when using httpclient as injected service use the service discovery
{
    http.AddServiceDiscovery();
});

builder.Services.AddDbContext<YvDataContext>(o => o.UseSqlServer(builder.Configuration.GetConnectionString("yvDatabase")));
builder.Services.AddScoped<IProductsRepository, EFProductsRepository>();

builder.Services.AddHttpClient("YVServerTouristApi", x=> x.BaseAddress   = new Uri("http://tApi"));
builder.Services.AddHttpClient("YVServerFoodApi", x => x.BaseAddress = new Uri("http://FApi"));

// Add as scoped
builder.Services.AddHttpClient<IOrdersService, OrdersService>(client =>
{
    client.BaseAddress = new Uri("https://paymentsapi");
}); 


builder.Services.AddEndpointsApiExplorer();

var app = builder.Build();
await app.Services.CreateScope().ServiceProvider.GetRequiredService<YvDataContext>().Database.EnsureCreatedAsync();

app.MapProductsApis();
app.MapConfigurationApis();
app.MapOrdersApis();

app.Run();





