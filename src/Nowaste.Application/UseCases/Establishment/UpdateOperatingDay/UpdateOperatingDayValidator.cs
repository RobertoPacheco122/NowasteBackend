using FluentValidation;
using Nowaste.Communication.Requests.Establishment;

namespace Nowaste.Application.UseCases.Establishment.UpdateOperatingDay;

public class UpdateOperatingDayValidator : AbstractValidator<RequestUpdateOperatingDayJson> {
    public UpdateOperatingDayValidator() {
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
    }
}
