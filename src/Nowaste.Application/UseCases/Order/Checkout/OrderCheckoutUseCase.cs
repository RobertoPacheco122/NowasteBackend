using Nowaste.Communication.Requests.Order;
using Nowaste.Communication.Responses.Order;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Order;
using Nowaste.Exception.ExceptionBase;
using Stripe.Checkout;

namespace Nowaste.Application.UseCases.Order.Checkout;

public class OrderCheckoutUseCase(IOrderReadOnlyRepository orderReadOnlyRepository)
    : IOrderCheckoutUseCase
{
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository = orderReadOnlyRepository;

    const string SUCCESS_URL =
        "http://localhost:3000/checkout/success?session_id={CHECKOUT_SESSION_ID}";
    const string CANCEL_URL = "http://localhost:3000/checkout/failure";
    const int TAX_PER_ORDER_IN_CENTS = 100;

    public async Task<ResponseOrderCheckoutJson> Execute(RequestOrderCheckoutJson request)
    {
        var orderEntity = await _orderReadOnlyRepository.GetById(request.OrderId);

        Validate(request, orderEntity);

        var session = await CreateCheckoutSessionInformations(orderEntity!);

        return new ResponseOrderCheckoutJson { SessionId = session.Id, SessionUrl = session.Url };
    }

    public static void Validate(RequestOrderCheckoutJson request, OrderEntity? order)
    {
        if (order is null)
            throw new NotFoundException("Pedido não encontrado.");

        var validationResult = new OrderCheckoutValidator().Validate(request);

        if (validationResult.IsValid is true)
            return;

        var errorsMessages = validationResult.Errors.Select(error => error.ErrorMessage).ToList();

        throw new ErrorOnValidationException(errorsMessages);
    }

    public static async Task<Session> CreateCheckoutSessionInformations(OrderEntity orderEntity)
    {
        var deliveryFeeInCents = orderEntity.Establishment.DeliveryFeeInCents;

        var lineItems = new List<SessionLineItemOptions>();

        foreach (var orderItem in orderEntity.OrderItems)
        {
            lineItems.Add(
                new SessionLineItemOptions
                {
                    PriceData = new SessionLineItemPriceDataOptions
                    {
                        Currency = "brl",
                        ProductData = new SessionLineItemPriceDataProductDataOptions
                        {
                            Name = orderItem.ProductName,
                        },
                        UnitAmount = orderItem.Total,
                    },
                    Quantity = orderItem.ItemQuantity,
                }
            );
        }

        lineItems.Add(
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "brl",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Description = "Valor de conveniência e processamento do pedido.",
                        Name = "Taxa de serviço",
                    },
                    UnitAmount = TAX_PER_ORDER_IN_CENTS,
                },
                Quantity = 1,
            }
        );

        lineItems.Add(
            new SessionLineItemOptions
            {
                PriceData = new SessionLineItemPriceDataOptions
                {
                    Currency = "brl",
                    ProductData = new SessionLineItemPriceDataProductDataOptions
                    {
                        Description = "Valor utilizado para cobrir os custos de entrega.",
                        Name = "Taxa de entrega",
                    },
                    UnitAmount = deliveryFeeInCents,
                },
                Quantity = 1,
            }
        );

        var options = new SessionCreateOptions
        {
            PaymentMethodTypes = ["card"],
            LineItems = lineItems,
            Mode = "payment",
            SuccessUrl = SUCCESS_URL,
            CancelUrl = CANCEL_URL,
            Metadata = new Dictionary<string, string> { { "order_id", orderEntity.Id.ToString() } },
        };

        return await new SessionService().CreateAsync(options);
    }
}
