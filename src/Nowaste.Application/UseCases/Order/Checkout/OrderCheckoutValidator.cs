using FluentValidation;
using Nowaste.Communication.Requests.Order;

namespace Nowaste.Application.UseCases.Order.Checkout;

public class OrderCheckoutValidator : AbstractValidator<RequestOrderCheckoutJson>
{
    public OrderCheckoutValidator()
    {
        RuleFor(order => order.OrderId)
            .NotEmpty()
            .WithMessage("A propriedade 'productId' de 'orderItems' é obrigatória.")
            .NotEqual(Guid.Empty)
            .WithMessage("O 'productId' de 'orderItems' é inválido.");
    }
}
