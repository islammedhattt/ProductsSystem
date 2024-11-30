using FluentValidation;

namespace Application.Features.Products.AddProduct
{
    public class UpdateProductValidator : AbstractValidator<UpdateProductRequest>
    {
        public UpdateProductValidator()
        {
            RuleFor(x => x.Id)
              .NotNull()
              .NotEmpty()
              .WithMessage(x => "Must be Provided");

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
