using dafstore.Application.ShoppingCarts.Abstractions.Repository;
using MediatR;

namespace dafstore.Application.ShoppingCarts.Commands.DeleteShoppingCart;

public record DeleteUserCommand(Guid Id) : IRequest<Guid>;

public class DeleteShoppingCartHandler : IRequestHandler<DeleteUserCommand, Guid>
{
    private readonly IShoppingCartRepository _repository;

    public DeleteShoppingCartHandler(IShoppingCartRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(DeleteUserCommand request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.FindByIdAsync(request.Id);

        if (shoppingCart is null) return Guid.Empty;

        await _repository.RemoveAsync(shoppingCart);
        await _repository.SaveChangesAsync();

        return shoppingCart.Id;
    }
}