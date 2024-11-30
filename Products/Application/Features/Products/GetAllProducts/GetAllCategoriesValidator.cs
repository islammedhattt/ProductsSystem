using FluentValidation;

namespace Application.Features.Products.GetAllProducts
{
    public sealed class GetAllProductsValidator : AbstractValidator<GetAllProductsRequest>
    {
        public GetAllProductsValidator() {
        }
    }
}
