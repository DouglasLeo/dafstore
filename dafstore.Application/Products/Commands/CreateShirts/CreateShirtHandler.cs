using dafstore.Application.Products.Abstractions.Repositories;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Enums;
using MediatR;

namespace dafstore.Application.Products.Commands.CreateShirts;

public record CreateShirtCommand(string Name,
    string Description,
    decimal Price,
    int Quantity,
    ESize Size,
    string[] Colors,
    IEnumerable<Category> Categories,
    EShirtCategory ShirtCategory,
    IEnumerable<string> Images) : IRequest<Guid>;

public class CreateShirtHandler : IRequestHandler<CreateShirtCommand, Guid>
{
    private readonly IShirtRepository _repository;

    public CreateShirtHandler(IShirtRepository repository)
    {
        _repository = repository;
    }
    
    public async Task<Guid> Handle(CreateShirtCommand request, CancellationToken cancellationToken)
    {
        var stockId = Guid.NewGuid();
        //TODO:Criar evento para enviar o item para a api que gerencia os precos
        
        var shirt = new Shirt(stockId, request.Name, request.Description, request.Price, request.Size, request.Colors,
            request.Categories, request.ShirtCategory, request.Images, request.Quantity > 0);

        await _repository.AddAsync(shirt);
        await _repository.SaveChangesAsync();

        return shirt.Id;
    }
}