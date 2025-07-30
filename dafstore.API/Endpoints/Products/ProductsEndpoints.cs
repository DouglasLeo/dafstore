using dafstore.API.Infrastructure;
using dafstore.Application.Products.Queries;

namespace dafstore.API.Endpoints.Products;

public abstract class ProductsEndpoints<T> : EndpointGroupBase where T : ProductDTO
{
    public override void Map(WebApplication app)
    {
        app.MapGroup(this)
            .RequireAuthorization();
    }
}