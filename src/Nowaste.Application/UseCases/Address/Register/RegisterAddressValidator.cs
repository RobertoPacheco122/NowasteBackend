using System.Text.RegularExpressions;
using FluentValidation;
using Nowaste.Communication.Requests.Address;

namespace Nowaste.Application.UseCases.Address.Register;

public partial class RegisterAddressValidator : AbstractValidator<RequestRegisterAddressJson>
{
    public RegisterAddressValidator()
    {
        RuleFor(address => address.StreetName).NotEmpty().WithMessage("O endereço é obrigatório.");

        RuleFor(address => address.Number)
            .NotEmpty()
            .WithMessage("O número do endereço é obrigatório.");

        RuleFor(address => address.City).NotEmpty().WithMessage("A cidade é obrigatória.");

        RuleFor(address => address.State).NotEmpty().WithMessage("O estado é obrigatória.");

        RuleFor(address => address.ZipCode)
            .NotEmpty()
            .WithMessage("O CEP é obrigatório.")
            .Must(zipCode => !HasAnyLetters().IsMatch(zipCode))
            .When(
                address => !string.IsNullOrWhiteSpace(address.ZipCode),
                ApplyConditionTo.CurrentValidator
            )
            .WithMessage("O CEP não pode conter letras.")
            .Length(8)
            .When(
                address => !string.IsNullOrWhiteSpace(address.ZipCode),
                ApplyConditionTo.CurrentValidator
            )
            .WithMessage("O CEP deve ter 8 caracteres.");

        RuleFor(x => x)
            .Must(HaveAtMostTwoIdsFilled)
            .WithMessage(
                "Não é permitido preencher mais de dois dos seguintes campos simultaneamente: PersonId, EstablishmentId, InstitutionId."
            );

        RuleFor(x => x)
            .Must(HaveAtLeastOneIdFilled)
            .WithMessage(
                "Pelo menos um dos seguintes campos deve ser preenchido: PersonId, EstablishmentId, ou InstitutionId."
            );
    }

    private static bool HaveAtMostTwoIdsFilled(RequestRegisterAddressJson address)
    {
        var filledCount = new List<Guid?>
        {
            address.InstitutionId,
            address.EstablishmentId,
            address.PersonId,
        }.Count(id => id.HasValue);

        return filledCount <= 2;
    }

    private bool HaveAtLeastOneIdFilled(RequestRegisterAddressJson address)
    {
        return new List<Guid?>
        {
            address.PersonId,
            address.EstablishmentId,
            address.InstitutionId,
        }.Any(id => id.HasValue);
    }

    [GeneratedRegex(@"[a-zA-Z]+")]
    private static partial Regex HasAnyLetters();
}
