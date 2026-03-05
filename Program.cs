using DataStructures.Adapters.Console;
using DataStructures.Application;
using DataStructures.Domain;
using DataStructures.Infrastructure;

var repository = new InMemoryFnbManagementRepository(
    new RestaurantProfile("The Hudson Rooms – Capella Hanoi", 40, 60),
    new[]
    {
        new DiningTable("T01", 4),
        new DiningTable("T02", 4),
        new DiningTable("T03", 2),
        new DiningTable("T04", 6),
        new DiningTable("T05", 6),
        new DiningTable("T06", 6),
        new DiningTable("T07", 4),
        new DiningTable("T08", 4),
        new DiningTable("T09", 8),
        new DiningTable("T10", 8),
    },
    new[]
    {
        new MenuItem("APP01", "Oyster Rockefeller", 260_000m),
        new MenuItem("MAIN01", "US Prime Ribeye", 950_000m),
        new MenuItem("MAIN02", "Atlantic Salmon", 520_000m),
        new MenuItem("DRINK01", "Signature Cocktail", 220_000m),
        new MenuItem("DRINK02", "Sparkling Water", 120_000m),
    });

var useCase = new RunHudsonRoomsFnbUseCase(repository);
var presenter = new FnbConsolePresenter();

var result = useCase.Execute();
presenter.Show(result);
