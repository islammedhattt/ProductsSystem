using MediatR;

namespace Application.Features.Products.AddProduct
{
    public class AddProductRequest : IRequest<AddProductResponse>
    {

        public string? Name { get; set; }
        public decimal? Price { get; set; }
        public string? Description { get; set; }
    }
}
