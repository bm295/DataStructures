using DataStructures.Domain;
using DomainOrder = DataStructures.Domain.Order;

namespace DataStructures.Application.Ports;

public interface IFnbReadPort
{
  Task<RestaurantProfile> GetProfileAsync(CancellationToken cancellationToken);
  Task<IReadOnlyList<DiningTable>> GetTablesAsync(CancellationToken cancellationToken);
  Task<IReadOnlyDictionary<string, MenuItem>> GetMenuAsync(CancellationToken cancellationToken);
  Task<IReadOnlyList<DomainOrder>> GetClosedOrdersAsync(CancellationToken cancellationToken);
}
