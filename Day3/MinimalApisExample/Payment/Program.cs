var builder = WebApplication.CreateBuilder(args);

var app = builder.Build();

app.MapPost("/DoPayment", async (PaymentRequest req) =>
{
    await Task.Delay(2000); // Simulate some processing delay
    return Results.Ok(new PaymentResponse("AAP912345"));
});


app.Run();
public record PaymentResponse(string PaymentCode);
public record PaymentRequest(decimal Amount, int UserId);


