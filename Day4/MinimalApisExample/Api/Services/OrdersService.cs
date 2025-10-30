namespace Api.Services;

public class OrdersService(HttpClient httpClient) : IOrdersService
{
    public async Task<PaymentResponse?> CompleteOrderAsync(PaymentRequest request)
    {
        //https://paymentsapi/DoPayment
        var response = await httpClient.PostAsJsonAsync("/DoPayment", request);
        
        if (response.IsSuccessStatusCode)
        {
            var paymentResponse = await response.Content.ReadFromJsonAsync<PaymentResponse>();
            return paymentResponse;
        }
        
        return null;
    }
}