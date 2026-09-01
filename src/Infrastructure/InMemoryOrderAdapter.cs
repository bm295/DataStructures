using DataStructures.Application.Ports;
using DataStructures.Domain;
using DomainOrder = DataStructures.Domain.Order;

namespace DataStructures.Infrastructure;

public sealed class InMemoryOrderAdapter(InMemoryFnbStore store) : IOrderPort
{
  public Task<DomainOrder?> FindByIdAsync(Guid orderId, CancellationToken cancellationToken)
  {
    store.Orders.TryGetValue(orderId, out var order);
    return Task.FromResult(order);
  }

  public Task<DomainOrder?> FindOpenOrderByTableAsync(string tableId, CancellationToken cancellationToken)
  {
    var order = store.Orders.Values
      .SingleOrDefault(x => x.TableId == tableId && x.Status is not OrderStatus.Closed);
    return Task.FromResult(order);
  }

  public Task SaveAsync(DomainOrder order, CancellationToken cancellationToken)
  {
    store.Orders[order.Id] = order;
    return Task.CompletedTask;
  }
}
