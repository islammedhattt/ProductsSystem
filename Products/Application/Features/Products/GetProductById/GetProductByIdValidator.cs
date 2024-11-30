using FluentValidation;

namespace Application.Features.Products.GetProductById
{
    public class GetProductByIdValidator : AbstractValidator<GetProductByIdRequest>
    {
        public GetProductByIdValidator() 
        {
            RuleFor(x => x.Id)
                .NotNull()
                .WithMessage("EmptyProductIDCheck");
        }
    }
}
