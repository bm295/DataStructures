using DataStructures.Domain;

namespace DataStructures.Infrastructure;

public sealed class InMemoryReservationRepository : IReservationRepository
{
    private readonly Dictionary<string, int> _reserved = new(StringComparer.Ordinal);

    public InMemoryReservationRepository(IEnumerable<string> skus)
    {
        foreach (var sku in skus)
        {
            _reserved[sku] = 0;
        }
    }

    public void AddReservation(string sku, int quantity)
    {
        _reserved[sku] = GetReserved(sku) + quantity;
    }

    public void ReleaseReservation(string sku, int quantity)
    {
        _reserved[sku] = GetReserved(sku) - quantity;
    }

    public void ClampToNonNegative(string sku)
    {
        if (GetReserved(sku) < 0)
        {
            _reserved[sku] = 0;
        }
    }

    public int GetReserved(string sku)
    {
        if (!_reserved.TryGetValue(sku, out var current))
        {
            throw new KeyNotFoundException($"Unknown sku: {sku}");
        }

        return current;
    }
}
