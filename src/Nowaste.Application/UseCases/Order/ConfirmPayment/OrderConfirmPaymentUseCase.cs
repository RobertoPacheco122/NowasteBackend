using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Order;
using Nowaste.Exception.ExceptionBase;
using Stripe;
using Stripe.Checkout;

namespace Nowaste.Application.UseCases.Order.ConfirmPayment;

public class OrderConfirmPaymentUseCase(
    IUnitOfWork unitOfWork,
    IOrderUpdateOnlyRepository orderUpdateOnlyRepository
) : IOrderConfirmPaymentUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IOrderUpdateOnlyRepository _orderUpdateOnlyRepository =
        orderUpdateOnlyRepository;

    public async Task Execute(Event stripeEvent)
    {
        if (stripeEvent.Type is not EventTypes.CheckoutSessionCompleted)
            return;

        var session = stripeEvent.Data.Object as Session;

        if (session is null)
            return;

        if (session.PaymentStatus is not "paid")
            return;

        if (session.Metadata is null || session.Metadata.Count is 0)
            throw new ErrorOnValidationException(["A propriedade de Metadata está vazia."]);

        var orderId = session.Metadata["order_id"];

        var orderEntity =
            await _orderUpdateOnlyRepository.GetById(Guid.Parse(orderId))
            ?? throw new NotFoundException("Pedido não encontrado.");

        orderEntity.IsPaid = true;
        orderEntity.OrderStatus = Domain.Enums.EOrderStatus.Confirmed;

        _orderUpdateOnlyRepository.Update(orderEntity);

        await _unitOfWork.Commit();
    }
}
