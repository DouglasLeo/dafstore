using AutoMapper;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Application.Products.Queries.GetPants;

public class PantsDTO : ProductDTO
{
    public EPantsTissueType TissueType { get; init; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Pants, PantsDTO>();
        }
    }
}