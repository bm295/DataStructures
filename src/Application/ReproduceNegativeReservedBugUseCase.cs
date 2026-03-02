using DataStructures.Domain;

namespace DataStructures.Application;

public sealed class ReproduceNegativeReservedBugUseCase(IReservationRepository repository)
{
    public ReproScenarioResult Execute()
    {
        var lines = new[]
        {
            new OrderLine("SP000003", 1),
            new OrderLine("SP000004", 1),
        };

        PlaceOrder(lines);
        CancelOrderBuggy(lines);

        var buggy = CreateSnapshot(lines);

        PlaceOrder(lines);
        CancelOrderFixed(lines);

        var fixedResult = CreateSnapshot(lines);

        return new ReproScenarioResult(buggy, fixedResult);
    }

    private void PlaceOrder(IEnumerable<OrderLine> lines)
    {
        foreach (var line in lines)
        {
            repository.AddReservation(line.Sku, line.Quantity);
        }
    }

    private void CancelOrderBuggy(IEnumerable<OrderLine> lines)
    {
        foreach (var line in lines)
        {
            repository.ReleaseReservation(line.Sku, line.Quantity);
            repository.ReleaseReservation(line.Sku, line.Quantity); // bug: release duplicated
        }
    }

    private void CancelOrderFixed(IEnumerable<OrderLine> lines)
    {
        foreach (var line in lines)
        {
            repository.ReleaseReservation(line.Sku, line.Quantity);
            repository.ClampToNonNegative(line.Sku);
        }
    }

    private IReadOnlyList<ReservationSnapshot> CreateSnapshot(IEnumerable<OrderLine> lines)
    {
        return lines
            .Select(l => l.Sku)
            .Distinct(StringComparer.Ordinal)
            .Select(sku => new ReservationSnapshot(sku, repository.GetReserved(sku)))
            .ToArray();
    }
}

public sealed record ReproScenarioResult(
    IReadOnlyList<ReservationSnapshot> Buggy,
    IReadOnlyList<ReservationSnapshot> Fixed);
