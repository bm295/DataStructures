using DataStructures.Domain;

namespace DataStructures.Application;

public sealed class RunHudsonRoomsFnbUseCase(IFnbManagementRepository repository)
{
    public ServiceSummary Execute()
    {
        ValidateCapacity();

        repository.OpenTable("T01", 4);
        repository.AddOrderItem("T01", "DRINK01", 4);
        repository.AddOrderItem("T01", "MAIN01", 4);

        repository.OpenTable("T03", 2);
        repository.AddOrderItem("T03", "APP01", 1);
        repository.AddOrderItem("T03", "MAIN02", 2);

        repository.OpenTable("T06", 6);
        repository.AddOrderItem("T06", "DRINK02", 6);
        repository.AddOrderItem("T06", "MAIN01", 6);

        var bills = new[]
        {
            repository.CloseTable("T01"),
            repository.CloseTable("T03"),
            repository.CloseTable("T06"),
        };

        return new ServiceSummary(
            repository.GetProfile(),
            repository.GetTables().Sum(t => t.Seats),
            bills.Sum(b => b.Guests),
            bills.Length,
            bills.Sum(b => b.Total),
            bills);
    }

    private void ValidateCapacity()
    {
        var profile = repository.GetProfile();
        var seats = repository.GetTables().Sum(t => t.Seats);

        if (seats < profile.MinSeats || seats > profile.MaxSeats)
        {
            throw new InvalidOperationException(
                $"Configured seats ({seats}) are outside required range {profile.MinSeats}-{profile.MaxSeats}.");
        }
    }
}
