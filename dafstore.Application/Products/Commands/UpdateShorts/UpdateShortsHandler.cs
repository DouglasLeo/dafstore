using dafstore.Application.Products.Abstractions.Repositories;
using dafstore.Domain.Contexts.ProductContext.Enums;
using MediatR;

namespace dafstore.Application.Products.Commands.UpdateShorts;

public record UpdateShortsCommand(
    Guid Id,
    string Name,
    string Description,
    decimal Price,
    ESize Size,
    string[] Colors) : IRequest<Guid>;

public class UpdateShortsHandler : IRequestHandler<UpdateShortsCommand, Guid>
{
    private readonly IShortsRepository _repository;

    public UpdateShortsHandler(IShortsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(UpdateShortsCommand request, CancellationToken cancellationToken)
    {
        var shorts = await _repository.FindByIdAsync(request.Id);
        if (shorts is null) return Guid.Empty;

        shorts.UpdateName(request.Name);
        shorts.UpdateDescription(request.Description);
        shorts.UpdatePrice(request.Price);
        shorts.UpdateSize(request.Size);
        shorts.UpdateColors(request.Colors);

        await _repository.UpdateAsync(shorts);
        await _repository.SaveChangesAsync();

        return shorts.Id;
    }
}