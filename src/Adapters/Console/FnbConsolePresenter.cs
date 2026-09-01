using DataStructures.Application.Models;

namespace DataStructures.Adapters.Console;

public sealed class FnbConsolePresenter
{
  public void Show(ServiceSummaryResult summary)
  {
    System.Console.WriteLine("=== FnB Management Demo ===");
    System.Console.WriteLine($"Restaurant: {summary.Profile.Name}");
    System.Console.WriteLine($"Seat requirement: {summary.Profile.MinSeats}-{summary.Profile.MaxSeats}");
    System.Console.WriteLine($"Configured seats: {summary.ConfiguredSeats}");
    System.Console.WriteLine();

    foreach (var bill in summary.Bills)
    {
      System.Console.WriteLine($"Order {bill.OrderId} - Table {bill.TableId} ({bill.Guests} guests)");
      foreach (var line in bill.Lines)
      {
        System.Console.WriteLine($" - {line.ItemName} x{line.Quantity} @ {line.UnitPrice:N0} = {line.LineTotal:N0}");
      }

      System.Console.WriteLine($" Payment: {bill.PaymentMethod} / Ref: {bill.PaymentReference}");
      System.Console.WriteLine($" Total: {bill.Total:N0}");
      System.Console.WriteLine();
    }

    System.Console.WriteLine("Daily summary");
    System.Console.WriteLine($"Orders closed: {summary.OrdersClosed}");
    System.Console.WriteLine($"Guests served: {summary.ServedGuests}");
    System.Console.WriteLine($"Revenue: {summary.Revenue:N0}");
  }
}
