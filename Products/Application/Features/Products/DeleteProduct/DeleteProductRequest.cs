using Application.Features.Products.AddProduct;
using MediatR;

namespace Application.Features.Products.DeleteProduct
{
    public class DeleteProductRequest : IRequest<DeleteProductResponse>
    {
        public Guid Id { get; set; }
    }
}
