using AutoMapper;
using dafstore.Domain.Contexts.ProductContext.Entities;
using dafstore.Domain.Contexts.ProductContext.Enums;

namespace dafstore.Application.Products.Queries.GetShorts;

public class ShortsDTO : ProductDTO
{
    public EShortsTissueType ShortsTissueType { get; init; }
    
    private class Mapping : Profile
    {
        public Mapping()
        {
            CreateMap<Shorts, ShortsDTO>();
        }
    }
}