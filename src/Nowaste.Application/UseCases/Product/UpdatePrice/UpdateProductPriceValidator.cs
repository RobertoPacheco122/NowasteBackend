using FluentValidation;
using Nowaste.Communication.Requests.Product;

namespace Nowaste.Application.UseCases.Product.UpdatePrice;

public class UpdateProductPriceValidator : AbstractValidator<RequestUpdateProductPriceJson> {
    public UpdateProductPriceValidator() {
        RuleFor(product => product.Price)
            .NotEmpty()
            .WithMessage("O preço do produto é obrigatório.")
            .When(product => product.Price <= 0, ApplyConditionTo.CurrentValidator)
            .WithMessage("O preço do produto deve ser maior que zero.");

        RuleFor(product => product.SalePrice)
            .NotEmpty()
            .WithMessage("O preço de venda do produto é obrigatório.")
            .When(product => product.SalePrice <= 0, ApplyConditionTo.CurrentValidator)
            .WithMessage("O preço de venda do produto deve ser maior que zero.");

        RuleFor(product => product.EffectiveDate)
            .NotEmpty()
            .WithMessage("A data de vigência do preço é obrigatória.")
            .When(product => product.EffectiveDate < DateTime.UtcNow, ApplyConditionTo.CurrentValidator)
            .WithMessage("A data de vigência do preço não pode ser no passado.");
    }
}
