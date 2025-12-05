using AutoMapper;
using dafstore.Application.Shared.Abstractions.Repositories;
using dafstore.Application.ShoppingCarts.Abstractions.Repository;
using dafstore.Application.ShoppingCarts.Queries;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ShoppingCartContext;
using MediatR;

namespace dafstore.Application.ShoppingCarts.Commands.UpdateShoppingCart;

public record UpdateShoppingCartCommand(Guid Id, List<ShoppingCartItemDTO> Items) : IRequest<Guid>;

public class UpdateShoppingCartHandler : IRequestHandler<UpdateShoppingCartCommand, Guid>
{
    private readonly IShoppingCartRepository _repository;
    private readonly IRepository<Product> _productRepository;
    private readonly IMapper _mapper;

    public UpdateShoppingCartHandler(IShoppingCartRepository repository, IRepository<Product> productRepository, IMapper mapper)
    {
        _repository = repository;
        _productRepository = productRepository;
        _mapper = mapper;
    }

    public async Task<Guid> Handle(UpdateShoppingCartCommand request, CancellationToken cancellationToken)
    {
        var shoppingCart = await _repository.GetByIdAsync(request.Id);
        if (shoppingCart is null) return Guid.Empty;

        var productIds = request.Items.Select(i => i.ProductId).ToList();

        var existingProducts = await _productRepository.FindAllByIdsAsync(productIds);

        var missingProducts = productIds.Except(existingProducts.Select(p => p.Id)).ToList();
        if (missingProducts.Count != 0)
            throw new Exception($"Invalid products: {string.Join(", ", missingProducts)}");

        shoppingCart.UpdateShoppingCartItems(_mapper.Map<List<ShoppingCartItem>>(request.Items));

        await _repository.UpdateAsync(shoppingCart);
        await _repository.SaveChangesAsync();

        return shoppingCart.Id;
    }
}