using Application.Common.Repositories;
using Application.Repositories;
using AutoMapper;
using MediatR;

namespace Application.Features.Products.AddProduct
{
    public class UpdateProductRequest : IRequest<UpdateProductResponse>
    {
        public Guid Id { get; set; }

        public string? Name { get; set; }
        public decimal? Price { get; set; } = 0;
        public string? Description { get; set; }

    }
}
