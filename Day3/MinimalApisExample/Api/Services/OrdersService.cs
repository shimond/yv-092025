namespace Api.Services;

public class OrdersService(HttpClient httpClient) : IOrdersService
{
    public async Task<PaymentResponse?> CompleteOrderAsync(PaymentRequest request)
    {
        var response = await httpClient.PostAsJsonAsync("/DoPayment", request);
        
        if (response.IsSuccessStatusCode)
        {
            var paymentResponse = await response.Content.ReadFromJsonAsync<PaymentResponse>();
            return paymentResponse;
        }
        
        return null;
    }
}