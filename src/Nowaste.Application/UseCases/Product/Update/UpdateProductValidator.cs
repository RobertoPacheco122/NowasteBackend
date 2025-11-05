using FluentValidation;
using Nowaste.Communication.Requests.Product;

namespace Nowaste.Application.UseCases.Product.Update;

public class UpdateProductValidator : AbstractValidator<RequestUpdateProductJson>
{
    public UpdateProductValidator()
    {
        RuleFor(product => product.Name).NotEmpty().WithMessage("O nome do produto é obrigatório.");

        RuleFor(product => product.WeightInGrams)
            .NotEmpty()
            .WithMessage("A propriedade 'WeightInGrams' é obrigatória.")
            .When(product => product.WeightInGrams <= 0, ApplyConditionTo.CurrentValidator)
            .WithMessage("A propriedade 'WeightInGrams' deve ser maior que zero.");

        RuleFor(product => product.InventoryTrackingType)
            .IsInEnum()
            .WithMessage("O tipo de controle de estoque é inválido.");

        When(
            product =>
                product.InventoryTrackingType
                is Communication.Enums.EProductInventoryTrackingType.ByUnit,
            () =>
            {
                RuleFor(product => product.QuantityInStock)
                    .NotEmpty()
                    .WithMessage(
                        "A propriedade 'quantityInStock' é obrigatória quando o tipo de controle de estoque for 'byUnit'."
                    )
                    .GreaterThanOrEqualTo(0)
                    .WithMessage("A propriedade 'QuantityInStock' deve ser maior ou igual a zero.");
            }
        );

        RuleFor(product => product.ProductCategoryId)
            .NotEmpty()
            .WithMessage("A propriedade 'productCategoryId' é obrigatória.");
    }
}
