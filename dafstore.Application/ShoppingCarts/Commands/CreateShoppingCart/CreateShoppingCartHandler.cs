using dafstore.Application.ShoppingCarts.Abstractions.Repository;
using dafstore.Domain.Contexts.ShoppingCartContext;
using MediatR;

namespace dafstore.Application.ShoppingCarts.Commands.CreateShoppingCart;

public record CreateShoppingCartCommand(
    Guid UserId,
    Guid ProductId,
    int Quantity,
    decimal Price)
    : IRequest<Guid>;

public class CreateShoppingCartHandler : IRequestHandler<CreateShoppingCartCommand, Guid>
{
    private readonly IShoppingCartRepository _repository;

    public CreateShoppingCartHandler(IShoppingCartRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var shoppingCart = new ShoppingCart(request.UserId);

        var firstShoppingCartItem =
            new ShoppingCartItem(shoppingCart.Id, request.ProductId, request.Quantity, request.Price);

        shoppingCart.AddShoppingCartItem(firstShoppingCartItem);

        await _repository.AddAsync(shoppingCart);
        await _repository.SaveChangesAsync();

        return shoppingCart.Id;
    }
}