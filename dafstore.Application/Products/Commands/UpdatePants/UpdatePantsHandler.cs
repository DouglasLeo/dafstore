using dafstore.Application.Products.Abstractions.Repositories;
using dafstore.Domain.Contexts.ProductContext.Enums;
using MediatR;

namespace dafstore.Application.Products.Commands.UpdatePants;

public record UpdatePantsCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    ESize Size,
    string[] Colors) : IRequest<Guid>;

public class UpdatePantsHandler : IRequestHandler<UpdatePantsCommand, Guid>
{
    private readonly IPantsRepository _repository;

    public UpdatePantsHandler(IPantsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(UpdatePantsCommand request, CancellationToken cancellationToken = default)
    {
        var pants = await _repository.FindByIdAsync(request.Id);
        if (pants is null) return Guid.Empty;

        pants.UpdateName(request.Name);
        pants.UpdateDescription(request.Description);
        pants.UpdatePrice(request.Price);
        pants.UpdateSize(request.Size);
        pants.UpdateColors(request.Colors);

        await _repository.UpdateAsync(pants);
        await _repository.SaveChangesAsync();

        return pants.Id;
    }
}