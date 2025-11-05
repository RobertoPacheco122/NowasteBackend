using AutoMapper;
using Nowaste.Communication.Responses.Order;
using Nowaste.Domain.Repositories.Order;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Order.GetById;

public class GetOrderByIdUseCase(IMapper mapper, IOrderReadOnlyRepository orderReadOnlyRepository)
    : IGetOrderByIdUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository = orderReadOnlyRepository;

    public async Task<ResponseGetOrderByIdJson> Execute(Guid orderId)
    {
        var orderEntity =
            await _orderReadOnlyRepository.GetById(orderId)
            ?? throw new NotFoundException("Pedido não encontrado.");

        return _mapper.Map<ResponseGetOrderByIdJson>(orderEntity);
    }
}
