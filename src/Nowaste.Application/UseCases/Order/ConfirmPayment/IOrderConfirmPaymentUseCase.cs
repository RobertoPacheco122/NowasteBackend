using Stripe;

namespace Nowaste.Application.UseCases.Order.ConfirmPayment;

public interface IOrderConfirmPaymentUseCase
{
    Task Execute(Event stripeEvent);
}
