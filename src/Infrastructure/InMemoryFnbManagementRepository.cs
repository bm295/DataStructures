using DataStructures.Domain;

namespace DataStructures.Infrastructure;

public sealed class InMemoryFnbManagementRepository : IFnbManagementRepository
{
    private readonly RestaurantProfile _profile;
    private readonly Dictionary<string, DiningTable> _tables;
    private readonly Dictionary<string, MenuItem> _menu;
    private readonly Dictionary<string, OpenTableState> _openedTables = new(StringComparer.Ordinal);

    public InMemoryFnbManagementRepository(
        RestaurantProfile profile,
        IEnumerable<DiningTable> tables,
        IEnumerable<MenuItem> menu)
    {
        _profile = profile;
        _tables = tables.ToDictionary(t => t.Id, StringComparer.Ordinal);
        _menu = menu.ToDictionary(m => m.Code, StringComparer.Ordinal);
    }

    public RestaurantProfile GetProfile() => _profile;

    public IReadOnlyList<DiningTable> GetTables() => _tables.Values.OrderBy(t => t.Id).ToArray();

    public IReadOnlyDictionary<string, MenuItem> GetMenu() => _menu;

    public void OpenTable(string tableId, int guests)
    {
        if (!_tables.TryGetValue(tableId, out var table))
        {
            throw new KeyNotFoundException($"Unknown table: {tableId}");
        }

        if (guests <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(guests), "Guests must be greater than zero.");
        }

        if (guests > table.Seats)
        {
            throw new InvalidOperationException($"Table {tableId} has {table.Seats} seats but received {guests} guests.");
        }

        if (_openedTables.ContainsKey(tableId))
        {
            throw new InvalidOperationException($"Table {tableId} is already opened.");
        }

        _openedTables[tableId] = new OpenTableState(guests);
    }

    public void AddOrderItem(string tableId, string menuCode, int quantity)
    {
        if (!_openedTables.TryGetValue(tableId, out var state))
        {
            throw new InvalidOperationException($"Table {tableId} is not opened.");
        }

        if (!_menu.ContainsKey(menuCode))
        {
            throw new KeyNotFoundException($"Unknown menu code: {menuCode}");
        }

        if (quantity <= 0)
        {
            throw new ArgumentOutOfRangeException(nameof(quantity), "Quantity must be greater than zero.");
        }

        state.AddItem(menuCode, quantity);
    }

    public TableBill CloseTable(string tableId)
    {
        if (!_openedTables.TryGetValue(tableId, out var state))
        {
            throw new InvalidOperationException($"Table {tableId} is not opened.");
        }

        var lines = state.Items
            .Select(i =>
            {
                var menuItem = _menu[i.Key];
                var lineTotal = menuItem.Price * i.Value;
                return new BillLine(menuItem.Name, i.Value, menuItem.Price, lineTotal);
            })
            .ToArray();

        var total = lines.Sum(l => l.LineTotal);

        _openedTables.Remove(tableId);
        return new TableBill(tableId, state.Guests, lines, total);
    }

    private sealed class OpenTableState(int guests)
    {
        public int Guests { get; } = guests;
        public Dictionary<string, int> Items { get; } = new(StringComparer.Ordinal);

        public void AddItem(string menuCode, int quantity)
        {
            Items[menuCode] = Items.GetValueOrDefault(menuCode) + quantity;
        }
    }
}
