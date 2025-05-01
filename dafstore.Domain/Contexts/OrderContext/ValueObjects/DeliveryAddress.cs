namespace dafstore.Domain.Contexts.OrderContext.ValueObjects;

public record DeliveryAddress(
    string CustomerName,
    string Street,
    string Number,
    string City,
    string State,
    string ZipCode,
    string Country);