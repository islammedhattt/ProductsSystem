using MediatR;

namespace Application.Features.Products.GetProductById
{
    public sealed record GetProductByIdRequest : IRequest<GetProductByIdResponse>
    {
        public Guid? Id { get; set; }
    }
}