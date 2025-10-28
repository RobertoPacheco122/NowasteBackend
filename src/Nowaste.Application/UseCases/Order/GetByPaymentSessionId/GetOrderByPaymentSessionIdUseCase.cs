using AutoMapper;
using Nowaste.Communication.Responses.Order;
using Nowaste.Domain.Repositories.Order;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Order.GetByPaymentSessionId;

public class GetOrderByPaymentSessionIdUseCase(
    IMapper mapper,
    IOrderReadOnlyRepository orderReadOnlyRepository
) : IGetOrderByPaymentSessionIdUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository = orderReadOnlyRepository;

    public async Task<ResponseGetOrderByIdJson> Execute(string paymentSessionId)
    {
        var orderEntity =
            await _orderReadOnlyRepository.GetByPaymentSessionId(paymentSessionId)
            ?? throw new NotFoundException("Pedido não encontrado.");

        return _mapper.Map<ResponseGetOrderByIdJson>(orderEntity);
    }
}
