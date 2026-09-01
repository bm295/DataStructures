using DomainOrder = DataStructures.Domain.Order;

namespace DataStructures.Application.Ports;

public interface IOrderPort
{
  Task<DomainOrder?> FindByIdAsync(Guid orderId, CancellationToken cancellationToken);
  Task<DomainOrder?> FindOpenOrderByTableAsync(string tableId, CancellationToken cancellationToken);
  Task SaveAsync(DomainOrder order, CancellationToken cancellationToken);
}
