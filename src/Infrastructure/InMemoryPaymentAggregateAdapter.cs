using DataStructures.Application.Ports;
using DomainPayment = DataStructures.Domain.Payments.Payment;

namespace DataStructures.Infrastructure;

public sealed class InMemoryPaymentAggregateAdapter(InMemoryFnbStore store) : IPaymentAggregatePort
{
  public Task<DomainPayment?> FindByOrderIdAsync(Guid orderId, CancellationToken cancellationToken)
  {
    var payment = store.Payments.Values.SingleOrDefault(x => x.OrderId == orderId);
    return Task.FromResult(payment);
  }

  public Task SaveAsync(DomainPayment payment, CancellationToken cancellationToken)
  {
    store.Payments[payment.PaymentId] = payment;
    return Task.CompletedTask;
  }
}
