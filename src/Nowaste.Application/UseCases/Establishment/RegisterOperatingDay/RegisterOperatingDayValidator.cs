using FluentValidation;
using Nowaste.Communication.Requests.Establishment;

namespace Nowaste.Application.UseCases.Establishment.RegisterOperatingDay;

public class RegisterOperatingDayValidator : AbstractValidator<RequestRegisterOperatingDayJson>
{
    public RegisterOperatingDayValidator()
    {
        RuleFor(operatingDay => operatingDay.DayOfWeek)
            .NotEmpty()
            .WithMessage("O dia da semana é obrigatório.")
            .IsInEnum()
            .WithMessage("Dia da semana inválido.");

        RuleFor(operatingDay => operatingDay.OpeningTime)
            .NotEmpty()
            .WithMessage("O horário de abertura é obrigatório.")
            .LessThan(operatingDay => operatingDay.ClosingTime)
            .WithMessage("O horário de abertura deve ser anterior ao horário de fechamento.");

        RuleFor(operatingDay => operatingDay.ClosingTime)
            .NotEmpty()
            .WithMessage("O horário de fechamento é obrigatório.")
            .GreaterThan(operatingDay => operatingDay.OpeningTime)
            .WithMessage("O horário de fechamento deve ser posterior ao horário de abertura.");

        RuleFor(operatingDay => operatingDay.EstablishmentId)
            .NotEmpty()
            .WithMessage("O ID do estabelecimento é obrigatório.");
    }
}
