using AutoMapper;
using Domain.Entities;

namespace Application.Features.Products.AddProduct
{
    public sealed class AddProductMapper : Profile
    {
        public AddProductMapper() 
        {
            CreateMap<AddProductRequest, Product>();
            CreateMap<Product, AddProductResponse>();
        }
    }
}
