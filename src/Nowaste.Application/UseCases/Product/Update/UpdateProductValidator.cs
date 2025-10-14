using FluentValidation;
using Nowaste.Communication.Requests.Product;

namespace Nowaste.Application.UseCases.Product.Update;

public class UpdateProductValidator : AbstractValidator<RequestUpdateProductJson>
{
    public UpdateProductValidator()
    {
        RuleFor(product => product.Name).NotEmpty().WithMessage("O nome do produto é obrigatório.");

        RuleFor(product => product.ProductCategoryId)
            .NotEmpty()
            .WithMessage("A propriedade 'productCategoryId' é obrigatória.");
    }
}
