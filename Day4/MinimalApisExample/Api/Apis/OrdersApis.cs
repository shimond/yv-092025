namespace Api.Apis;

public static class OrdersApis
{
    public static void MapOrdersApis(this IEndpointRouteBuilder app)
    {
        var configGroup = app.MapGroup("/api/orders");
        configGroup.MapPost("CompleteOrder", CompleteOrders);
        configGroup.MapPost("UseMoreThan1HttpClient", UseMoreThan1HttpClient);
    }

    private static  object UseMoreThan1HttpClient(IHttpClientFactory factory)
    {
        var clientT = factory.CreateClient("YVServerTouristApi");
        var clientF = factory.CreateClient("YVServerFoodApi");
        return new { AddressT = clientT.BaseAddress?.ToString(), AddressF = clientF.BaseAddress?.ToString() };
            
    }

    static async Task<IResult> CompleteOrders(IOrdersService ordersService)
    {
        var paymentRequest = new PaymentRequest(202, 3);
        var result = await ordersService.CompleteOrderAsync(paymentRequest);
        
        if (result != null)
        {
            return Results.Ok(result);
        }
        
        return Results.BadRequest("Payment failed");
    }
}

//1. Save url of the server in config
//2. Working with BaseUrl
//3. Declare Data model for request and response
//4. create new http client each time is not a good idea

//HttpClient client = new HttpClient();