using DomainPayment = DataStructures.Domain.Payments.Payment;

namespace DataStructures.Application.Ports;

public interface IPaymentAggregatePort
{
  Task<DomainPayment?> FindByOrderIdAsync(Guid orderId, CancellationToken cancellationToken);
  Task SaveAsync(DomainPayment payment, CancellationToken cancellationToken);
}
