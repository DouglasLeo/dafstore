using AutoMapper;
using dafstore.Domain.Contexts.ShoppingCartContext;

namespace dafstore.Application.ShoppingCarts.Queries;

public class ShoppingCartDTO
{
    public Guid Id { get; init; }

    public IReadOnlyCollection<ShoppingCartItemDTO> ShoppingCartItems { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ShoppingCart, ShoppingCartDTO>();
        }
    }
}

public class ShoppingCartItemDTO
{
    public Guid ShoppingCartId { get; init; }
    public Guid ProductId { get; init; }
    public int Quantity { get; init; }
    public decimal Price { get; init; }

    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<ShoppingCartItem, ShoppingCartItemDTO>();
        }
    }
}