using AutoMapper;
using Domain.Entities;

namespace Application.Features.Products.DeleteProduct
{
    public class DeleteProductMapper : Profile
    {
        public DeleteProductMapper() 
        {
            CreateMap<DeleteProductRequest, Product>();
            CreateMap<Product, DeleteProductResponse>();
        }
    }
}
