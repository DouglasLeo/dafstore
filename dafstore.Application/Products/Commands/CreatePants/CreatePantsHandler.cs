using dafstore.Application.Products.Abstractions.Repositories;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Enums;
using MediatR;

namespace dafstore.Application.Products.Commands.CreatePants;

public record CreatePantsCommand(
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    ESize Size,
    string[] Colors,
    IEnumerable<Category> Categories,
    EPantsTissueType TissueType,
    IEnumerable<string> Images) : IRequest<Guid>;

public class CreatePantsHandler : IRequestHandler<CreatePantsCommand, Guid>
{
    private readonly IPantsRepository _repository;

    public CreatePantsHandler(IPantsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreatePantsCommand request, CancellationToken cancellationToken = default)
    {
        var stockId = Guid.NewGuid();
        //TODO:Criar evento para enviar o item para a api que gerencia os precos
        
        var pants = new Pants(stockId, request.Name, request.Description, request.Price, request.Size, request.Colors,
            request.Categories, request.TissueType, request.Images, request.Quantity > 0);

        await _repository.AddAsync(pants);
        await _repository.SaveChangesAsync();

        return pants.Id;
    }
}