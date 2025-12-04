using AutoMapper;
using dafstore.Domain.Contexts.OrderContext.ValueObjects;

namespace dafstore.Application.Orders.Commands;

public record DeliveryAddressDTO(
    string CustomerName,
    string Street,
    string Number,
    string City,
    string State,
    string ZipCode,
    string Country)
{
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<DeliveryAddressDTO, DeliveryAddress>();
        }
    }
}