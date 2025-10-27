using AutoMapper;
using Nowaste.Communication.Requests.Order;
using Nowaste.Communication.Responses.Order;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Enums;
using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Domain.Repositories.Establishment;
using Nowaste.Domain.Repositories.Order;
using Nowaste.Domain.Repositories.Product;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Order.Register;

public class RegisterOrderUseCase(
    IUnitOfWork unitOfWork,
    IMapper mapper,
    ILoggedUser loggedUser,
    IOrderWriteOnlyRepository orderWriteOnlyRepository,
    IOrderReadOnlyRepository orderReadOnlyRepository,
    IProductReadOnlyRepository productReadOnlyRepository,
    IEstablishmentReadOnlyRepository establishmentReadOnlyRepository,
    IAddressReadOnlyRepository addressReadOnlyRepository
) : IRegisterOrderUseCase
{
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IOrderWriteOnlyRepository _orderWriteOnlyRepository = orderWriteOnlyRepository;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository = orderReadOnlyRepository;
    private readonly IProductReadOnlyRepository _productReadOnlyRepository =
        productReadOnlyRepository;
    private readonly IEstablishmentReadOnlyRepository _establishmentReadOnlyRepository =
        establishmentReadOnlyRepository;
    private readonly IAddressReadOnlyRepository _addressReadOnlyRepository =
        addressReadOnlyRepository;

    const int TAX_PER_ORDER = 100;

    public async Task<ResponseRegisteredOrderJson> Execute(RequestRegisterOrderJson request)
    {
        Validate(request);

        var loggedUser =
            await _loggedUser.Get() ?? throw new NotFoundException("Usuário não encontrado.");

        var establishmentEntity =
            await _establishmentReadOnlyRepository.GetById(request.EstablishmentId)
            ?? throw new NotFoundException("Estabelecimento não encontrado.");

        var addressEntity =
            await _addressReadOnlyRepository.GetById(request.AddressId)
            ?? throw new NotFoundException("Endereço não encontrado.");

        var establishmentOrdersCount = await _orderReadOnlyRepository.GetOrdersCountByEstablishment(
            request.EstablishmentId
        );

        var orderEntity = _mapper.Map<OrderEntity>(request);
        orderEntity.CreatedAt = DateTime.UtcNow;
        orderEntity.Id = Guid.NewGuid();
        orderEntity.Tax = TAX_PER_ORDER;
        orderEntity.OrderStatus = EOrderStatus.Pending;

        var allPurchasedProductsIds = request.Items.Select(item => item.ProductId).ToList();

        var allPurchasedProductsEntities = await _productReadOnlyRepository.GetAllByIds(
            allPurchasedProductsIds
        );

        var orderItemsEntities = GetAllOrderItemsFormattedForPersistence(
            request,
            allPurchasedProductsEntities,
            orderEntity.Id
        );

        var orderItemsSubtotal = orderItemsEntities.Sum(order => order.Subtotal);
        var orderItemsDiscount = orderItemsEntities.Sum(order => order.Discount);
        var orderItemsTotal = orderItemsEntities.Sum(order => order.Total);
        var orderTotal = orderItemsTotal + establishmentEntity.DeliveryFeeInCents + orderEntity.Tax;

        var fullDeliveryAddress =
            $"{addressEntity.StreetName}, {addressEntity.Number} - {addressEntity.Neighborhood}"
            + $" - {addressEntity.City}/{addressEntity.State}";

        var orderFriendlyId = establishmentOrdersCount + 1;

        orderEntity.Subtotal = orderItemsSubtotal;
        orderEntity.Discount = orderItemsDiscount;
        orderEntity.DeliveryFee = establishmentEntity.DeliveryFeeInCents;
        orderEntity.Total = orderTotal;

        orderEntity.DeliveryAddress = fullDeliveryAddress;
        orderEntity.FriendlyId = orderFriendlyId.ToString();
        orderEntity.PersonId = loggedUser.Person.Id;

        await _orderWriteOnlyRepository.Add(orderEntity);

        await _orderWriteOnlyRepository.AddManyOrderItems(orderItemsEntities);

        await _unitOfWork.Commit();

        return new ResponseRegisteredOrderJson
        {
            OrderId = orderEntity.Id,
            OrderStatus = (Communication.Enums.EOrderStatus)EOrderStatus.Pending,
        };
    }

    public static void Validate(RequestRegisterOrderJson request)
    {
        var validationResult = new RegisterOrderValidator().Validate(request);

        if (validationResult.IsValid is false)
        {
            var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage).ToList();

            throw new ErrorOnValidationException(errorMessages);
        }
    }

    public static List<OrderItemEntity> GetAllOrderItemsFormattedForPersistence(
        RequestRegisterOrderJson request,
        ICollection<ProductEntity> allPurchasedProductsEntities,
        Guid orderId
    )
    {
        var orderItemsEntities = new List<OrderItemEntity>();

        foreach (var item in request.Items)
        {
            var productEntity =
                allPurchasedProductsEntities
                    .Where(product => product.Id.Equals(item.ProductId))
                    .FirstOrDefault()
                ?? throw new NotFoundException("Um dos produtos nâo foi encontrado.");

            var actualPriceHistory =
                productEntity
                    .PriceHistories.Where(priceHistory =>
                        priceHistory.EffectiveDate <= DateTime.UtcNow
                    )
                    .OrderByDescending(priceHistory => priceHistory.EffectiveDate)
                    .FirstOrDefault()
                ?? throw new NotFoundException("O preço de um dos produtos não foi encontrado.");

            var subtotal = item.ItemQuantity * actualPriceHistory.Price;
            var discount = subtotal - (item.ItemQuantity * actualPriceHistory.SalePrice);
            var total = discount is 0 ? subtotal : subtotal - discount;

            orderItemsEntities.Add(
                new OrderItemEntity
                {
                    CreatedAt = DateTime.UtcNow,
                    ProductName = productEntity.Name,
                    UnitPrice = actualPriceHistory.Price,
                    ItemQuantity = item.ItemQuantity,
                    Subtotal = subtotal,
                    Discount = discount,
                    Total = total,
                    OrderId = orderId,
                    ProductId = productEntity.Id,
                }
            );
        }

        return orderItemsEntities;
    }
}
