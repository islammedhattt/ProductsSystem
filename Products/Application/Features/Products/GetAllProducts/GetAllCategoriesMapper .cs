using AutoMapper;
using Domain.Entities;

namespace Application.Features.Products.GetAllProducts
{
    public sealed class GetAllProductsMapper : Profile
    {
        public GetAllProductsMapper()
        {
            CreateMap<GetAllProductsRequest, Product>();
            CreateMap<Product, GetAllProductsResponse>();
        }
    }
}