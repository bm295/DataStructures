using DataStructures.Domain;

namespace DataStructures.Application.Models;

public sealed record CreateOrderCommand(string TableId, int Guests);

public sealed record AddItemCommand(Guid OrderId, string MenuCode, int Quantity);

public sealed record RemoveItemCommand(Guid OrderId, string MenuCode, int Quantity);

public sealed record ProcessPaymentCommand(Guid OrderId, PaymentMethod Method);
