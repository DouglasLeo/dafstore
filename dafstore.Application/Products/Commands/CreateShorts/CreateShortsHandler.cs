using dafstore.Application.Products.Abstractions.Repositories;
using dafstore.Application.Products.Commands.Shared;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Enums;
using dafstore.Domain.Contexts.ProductContext.ValueObjects;
using MediatR;

namespace dafstore.Application.Products.Commands.CreateShorts;

public record CreateShortsCommand(
    string Name,
    string Description,
    decimal Price,
    int Quantity,
    ESize Size,
    string[] Colors,
    IEnumerable<CategoryDTO> Categories,
    EShortsTissueType TissueType,
    IEnumerable<string> Images) : IRequest<Guid>;

public class CreateShortsHandler : IRequestHandler<CreateShortsCommand, Guid>
{
    private readonly IShortsRepository _repository;

    public CreateShortsHandler(IShortsRepository repository)
    {
        _repository = repository;
    }

    public async Task<Guid> Handle(CreateShortsCommand request, CancellationToken cancellationToken)
    {
        var stockId = Guid.NewGuid();
        //TODO:Criar evento para enviar o item para a api que gerencia os precos

        var categories = request.Categories.Select(c =>
            new Category(new CategoryName(c.Name), c.Description));

        var pants = new Shorts(stockId, request.Name, request.Description, request.Price, request.Size,
            request.TissueType, request.Colors,
            categories, request.Images, request.Quantity > 0);

        await _repository.AddAsync(pants);
        await _repository.SaveChangesAsync();

        return pants.Id;
    }
}