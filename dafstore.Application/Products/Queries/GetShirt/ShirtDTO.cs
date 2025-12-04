using AutoMapper;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Application.Products.Queries.GetShirt;

public class ShirtDTO : ProductDTO
{
    public EShirtCategory ShirtCategory { get; init; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Shirt, ShirtDTO>();
        }
    }
}