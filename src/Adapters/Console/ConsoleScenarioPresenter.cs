using DataStructures.Application;

namespace DataStructures.Adapters.Console;

public sealed class ConsoleScenarioPresenter
{
    public void Show(ReproScenarioResult result)
    {
        Console.WriteLine("=== Reproduce negative 'Đặt hàng' bug ===");
        Console.WriteLine("Ticket context: SKUs SP000003, SP000004 become -1 after cancellation.");
        Console.WriteLine();

        Console.WriteLine("Buggy flow result:");
        Print(result.Buggy);

        Console.WriteLine();

        Console.WriteLine("Fixed flow result:");
        Print(result.Fixed);
    }

    private static void Print(IEnumerable<DataStructures.Domain.ReservationSnapshot> snapshots)
    {
        foreach (var item in snapshots)
        {
            Console.WriteLine($"{item.Sku} Đặt hàng = {item.Reserved}");
        }
    }
}
