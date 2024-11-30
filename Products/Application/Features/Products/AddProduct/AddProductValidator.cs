using FluentValidation;

namespace Application.Features.Products.AddProduct
{
    public class AddProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public AddProductValidator()
        {
            RuleFor(x => x.Name)
                .NotNull()
                .NotEmpty()
                .WithMessage(x => x.Name);


            RuleFor(x => x.Description)
                .MaximumLength(200)
                .WithMessage("Maximum Length 200 characters");
        }
    }
}
