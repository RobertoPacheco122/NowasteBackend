using FluentValidation;
using Nowaste.Communication.Requests.Order;

namespace Nowaste.Application.UseCases.Order.Register;

public class RegisterOrderValidator : AbstractValidator<RequestRegisterOrderJson> {
    public RegisterOrderValidator() {
        RuleFor(order => order.AddressId)
            .NotEmpty()
            .WithMessage("A propriedade 'addressId' é obrigatória.")
            .NotEqual(Guid.Empty)
            .WithMessage("A propriedade 'addressId' não pode ser um UUID vazio.");

        RuleFor(order => order.EstablishmentId)
            .NotEmpty()
            .WithMessage("A propriedade 'establishmentId' é obrigatória")
            .NotEqual(Guid.Empty)
            .WithMessage("A propriedade 'establishmentId' não pode ser um UUID vazio.");

        RuleFor(order => order.Items)
            .NotEmpty()
            .WithMessage("A propriedade 'items' não pode ser vazia.");

        RuleForEach(order => order.Items)
            .ChildRules(item => {
                item.RuleFor(i => i.ProductId)
                    .NotEmpty()
                    .WithMessage("A propriedade 'productId' de 'items' é obrigatória.")
                    .NotEqual(Guid.Empty)
                    .WithMessage("A propriedade 'productId' de 'items' não pode ser um UUID vazio.");

                item.RuleFor(i => i.ItemQuantity)
                    .NotEmpty()
                    .WithMessage("A propriedade 'itemQuantity' de 'items' é obrigatória.")
                    .GreaterThan(0)
                    .WithMessage("A propriedade 'itemQuantity' de 'items' deve ser maior que zero.");
            });

        RuleFor(order => order.OrderDate)
            .NotEmpty()
            .WithMessage("A propriedade 'orderDate' é obrigatória.")
            .LessThanOrEqualTo(DateTime.UtcNow)
            .WithMessage("A propriedade 'orderDate' não pode ser maior que a data atual");

        RuleFor(order => order.PaymentMethod)
            .NotEmpty()
            .WithMessage("A propriedade 'paymentMethod' é obrigatória.")
            .IsInEnum()
            .WithMessage("A propriedade 'paymentMethod' é inválida.");
    }
}
