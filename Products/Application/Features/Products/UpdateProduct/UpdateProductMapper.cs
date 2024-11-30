using AutoMapper;
using Domain.Entities;

namespace Application.Features.Products.AddProduct
{
    public class UpdateProductMapper : Profile
    {
        public UpdateProductMapper() 
        {
            CreateMap<UpdateProductRequest, Product>();
        }
    }
}
