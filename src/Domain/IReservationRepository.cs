namespace DataStructures.Domain;

public interface IReservationRepository
{
    void AddReservation(string sku, int quantity);
    void ReleaseReservation(string sku, int quantity);
    void ClampToNonNegative(string sku);
    int GetReserved(string sku);
}
