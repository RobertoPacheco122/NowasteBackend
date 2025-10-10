using FluentValidation;
using Nowaste.Communication.Requests.Product;

namespace Nowaste.Application.UseCases.Product.Register;

public class RegisterProductValidator : AbstractValidator<RequestRegisterProductJson> {
    public RegisterProductValidator() {
        RuleFor(product => product.Name)
            .NotEmpty()
            .WithMessage("O nome é obrigatório.");

        RuleFor(product => product.ProductCategoryId)
            .NotEmpty()
            .WithMessage("A propriedade 'productCategoryId' é obrigatória.");

        RuleFor(product => product.EstablishmentId)
            .NotEmpty()
            .WithMessage("A propriedade 'establishmentId' é obrigatória.");

        RuleFor(product => product.Price)
            .NotEmpty()
            .WithMessage("O preço é obrigatório.")
            .When(product => product.Price <= 0, ApplyConditionTo.CurrentValidator)
            .WithMessage("O preço não pode ser menor ou igual a zero.");

        RuleFor(product => product.InventoryTrackingType)
            .IsInEnum()
            .WithMessage("O tipo de controle de estoque é inválido.");
    }
}
