using FluentValidation;
using Nowaste.Communication.Requests.Product;

namespace Nowaste.Application.UseCases.Product.RegisterCategory;

public class RegisterProductCategoryValidator : AbstractValidator<RequestRegisterProductCategoryJson> {
    public RegisterProductCategoryValidator() {
        RuleFor(productCategory => productCategory.Name)
            .NotEmpty()
            .WithMessage("O nome da categoria é obrigatório.");
    }
}
