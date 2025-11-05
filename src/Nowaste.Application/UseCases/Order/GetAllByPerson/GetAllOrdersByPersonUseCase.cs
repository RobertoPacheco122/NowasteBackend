using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using AutoMapper;
using Nowaste.Communication.Responses.Order;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Order;
using Nowaste.Domain.Services.LoggedUser;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Order.GetAllByPerson;

public class GetAllOrdersByPersonUseCase(
    IMapper mapper,
    ILoggedUser loggedUser,
    IOrderReadOnlyRepository orderReadOnlyRepository
) : IGetAllOrdersByPersonUseCase
{
    private readonly IMapper _mapper = mapper;
    private readonly ILoggedUser _loggedUser = loggedUser;
    private readonly IOrderReadOnlyRepository _orderReadOnlyRepository = orderReadOnlyRepository;

    public async Task<ICollection<ResponseGetOrderByIdJson>> Execute()
    {
        var loggedUser = await _loggedUser.Get();

        Validate(loggedUser);

        var ordersEntities = await _orderReadOnlyRepository.GetAllByPerson(loggedUser.Person.Id);

        if (ordersEntities.Count == 0)
            throw new NotFoundException("Nenhum pedido encontrado para este usuário.");

        return _mapper.Map<ICollection<ResponseGetOrderByIdJson>>(ordersEntities);
    }

    public static void Validate(UserEntity loggedUser)
    {
        if (loggedUser == null)
            throw new NotFoundException("Usuário não encontrado.");
    }
}
