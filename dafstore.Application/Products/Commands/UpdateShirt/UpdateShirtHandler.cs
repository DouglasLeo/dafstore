using dafstore.Application.Products.Abstractions.Repositories;
using dafstore.Domain.Contexts.ProductContext.Enums;
using MediatR;

namespace dafstore.Application.Products.Commands.UpdateShirt;

public record UpdateShirtCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    ESize Size,
    string[] Colors) : IRequest<Guid>;

public class UpdateShirtHandler : IRequestHandler<UpdateShirtCommand, Guid>
{
    private readonly IShirtRepository _repository;

    public UpdateShirtHandler(IShirtRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(UpdateShirtCommand request, CancellationToken cancellationToken)
    {
        var shirt = await _repository.FindByIdAsync(request.Id);
        if (shirt is null) return Guid.Empty;

        shirt.UpdateName(request.Name);
        shirt.UpdateDescription(request.Description);
        shirt.UpdatePrice(request.Price);
        shirt.UpdateSize(request.Size);
        shirt.UpdateColors(request.Colors);

        await _repository.UpdateAsync(shirt);
        await _repository.SaveChangesAsync();

        return shirt.Id;
    }
}