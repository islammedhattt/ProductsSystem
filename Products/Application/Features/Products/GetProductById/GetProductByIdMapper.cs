using AutoMapper;
using Domain.Entities;

namespace Application.Features.Products.GetProductById
{
    public class GetProductByIdMapper : Profile
    {
        public GetProductByIdMapper()
        {
            CreateMap<GetProductByIdRequest, Product>();
            CreateMap<Product, GetProductByIdResponse>();
        }
    }
}
