namespace DataStructures.Domain;

public sealed record RestaurantProfile(string Name, int MinSeats, int MaxSeats);

public sealed record DiningTable(string Id, int Seats);

public sealed record MenuItem(string Code, string Name, decimal Price);

public sealed record OrderItem(string MenuCode, int Quantity);

public sealed record BillLine(string ItemName, int Quantity, decimal UnitPrice, decimal LineTotal);

public sealed record TableBill(string TableId, int Guests, IReadOnlyList<BillLine> Lines, decimal Total);

public sealed record ServiceSummary(
    RestaurantProfile Profile,
    int ConfiguredSeats,
    int ServedGuests,
    int OrdersClosed,
    decimal Revenue,
    IReadOnlyList<TableBill> Bills);
