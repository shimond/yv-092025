using Api.Model.Dtos.Requests;
using Api.Model.Dtos.Responses;

namespace Api.Contracts;

public interface IOrdersService
{
    Task<PaymentResponse?> CompleteOrderAsync(PaymentRequest request);
}