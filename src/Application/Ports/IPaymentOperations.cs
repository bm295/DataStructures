using DataStructures.Application.Models;
using DataStructures.Domain;
using DomainOrder = DataStructures.Domain.Order;

namespace DataStructures.Application.Ports;

public interface IPaymentOperations
{
  Task<PaymentResult> ChargeOrderAsync(
    DomainOrder order,
    PaymentMethod method,
    Guid paymentAttemptId,
    CancellationToken cancellationToken = default);
}
