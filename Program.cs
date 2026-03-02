using DataStructures.Adapters.Console;
using DataStructures.Application;
using DataStructures.Infrastructure;

var repository = new InMemoryReservationRepository(new[] { "SP000003", "SP000004" });
var useCase = new ReproduceNegativeReservedBugUseCase(repository);
var presenter = new ConsoleScenarioPresenter();

var result = useCase.Execute();
presenter.Show(result);
