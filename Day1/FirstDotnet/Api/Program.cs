var builder = WebApplication.CreateBuilder(args);

//Configurations and services



builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(policy =>
    {
        policy.AllowAnyOrigin()
              .AllowAnyHeader()
              .AllowAnyMethod();
    });
});



var app = builder.Build();

app.UseCors(); // listen to method options only

app.UseStaticFiles();

// middleware
app.Use(async (context, next) => {
    context.Response.StatusCode = 200;
    await context.Response.WriteAsync("  Middleware 1 A  "); // 1
    if (context.Request.Method.ToUpper() == "POST")
    {
        await next();
    }
    await context.Response.WriteAsync("  Middleware 1 B  "); // 6
});

//app.Use(async (context, next) => {
//    await context.Response.WriteAsync("  Middleware 2 A  "); // 2
//    await next();
//    await context.Response.WriteAsync("  Middleware 2 B  "); //5
//});
// as middleware class
app.UseLoggingMiddleware();


app.Use(async (context, next) => {

    await context.Response.WriteAsync("  Middleware 3 A  "); //3
    await next();
    await context.Response.WriteAsync("  Middleware 3 B  "); //4
});


app.Run();


// Authentication
