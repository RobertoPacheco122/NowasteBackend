using FluentValidation;
using Nowaste.Communication.Requests.Establishment;

namespace Nowaste.Application.UseCases.Establishment.Register;

public class RegisterEstablishmentValidator : AbstractValidator<RequestRegisterEstablishmentJson>
{
    public RegisterEstablishmentValidator()
    {
        RuleFor(establishment => establishment.Cnpj)
            .NotEmpty()
            .WithMessage("O CNPJ é obrigatório.")
            .Length(14)
            .When(
                establishment => string.IsNullOrWhiteSpace(establishment.Cnpj) is false,
                ApplyConditionTo.CurrentValidator
            )
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

        RuleFor(establishment => establishment.DeliveryFeeInCents)
            .LessThan(0)
            .WithMessage("A propriedade 'deliveryFeeInCents' não pode ser menor que 0 (zero).");

        RuleFor(establishment => establishment.Email)
            .NotEmpty()
            .WithMessage("O email é obrigatório.")
            .EmailAddress()
            .When(
                establishment => string.IsNullOrWhiteSpace(establishment.Email) is false,
                ApplyConditionTo.CurrentValidator
            )
            .WithMessage("O email não é válido.");
    }
}
