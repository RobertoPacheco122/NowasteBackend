using FluentValidation;
using Nowaste.Communication.Requests.Establishments;

namespace Nowaste.Application.UseCases.Establishments.Register;

public class RegisterEstablishmentValidator : AbstractValidator<RequestRegisterEstablishmentJson> {
    public RegisterEstablishmentValidator() {
        RuleFor(establishment => establishment.Cnpj)
            .NotEmpty()
            .WithMessage("O CNPJ é obrigatório.")

            .MinimumLength(14)
            .When(establishment => string.IsNullOrWhiteSpace(establishment.Cnpj) is false, ApplyConditionTo.CurrentValidator)
            .WithMessage("O CNPJ deve ter 14 caracteres.")

            .MaximumLength(14)
            .When(establishment => string.IsNullOrWhiteSpace(establishment.Cnpj) is false, ApplyConditionTo.CurrentValidator)
            .WithMessage("O CNPJ deve ter 14 caracteres.");

        RuleFor(establishment => establishment.LegalName)
            .NotEmpty()
            .WithMessage("A Razão Social é obrigatória.");

        RuleFor(establishment => establishment.TradeName)
            .NotEmpty()
            .WithMessage("O Nome Fantasia é obrigatório.");

        RuleFor(establishment => establishment.ExhibitionName)
            .NotEmpty()
            .WithMessage("O nome de exibição é obrigatório");

        RuleFor(establishment => establishment.Email)
            .NotEmpty()
            .WithMessage("O email é obrigatório.")
            .EmailAddress()
            .When(establishment => string.IsNullOrWhiteSpace(establishment.Email) is false, ApplyConditionTo.CurrentValidator)
            .WithMessage("O email não é válido.");
    }
}
