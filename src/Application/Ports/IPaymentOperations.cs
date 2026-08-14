using DataStructures.Application.Models;
using DataStructures.Domain;

namespace DataStructures.Application.Ports;

public interface IPaymentOperations
{
  Task<PaymentResult> ChargeOrderAsync(
    Order order,
    PaymentMethod method,
    Guid paymentAttemptId,
    CancellationToken cancellationToken = default);
}
