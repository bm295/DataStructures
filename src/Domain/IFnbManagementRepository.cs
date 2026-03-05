namespace DataStructures.Domain;

public interface IFnbManagementRepository
{
    RestaurantProfile GetProfile();
    IReadOnlyList<DiningTable> GetTables();
    IReadOnlyDictionary<string, MenuItem> GetMenu();

    void OpenTable(string tableId, int guests);
    void AddOrderItem(string tableId, string menuCode, int quantity);
    TableBill CloseTable(string tableId);
}
