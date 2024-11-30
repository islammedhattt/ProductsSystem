using MediatR;

namespace Application.Features.Products.GetAllProducts
{
    public sealed record GetAllProductsRequest(
     ) : IRequest<List<GetAllProductsResponse>>;
}