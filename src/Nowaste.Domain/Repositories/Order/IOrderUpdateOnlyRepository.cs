using Nowaste.Domain.Entities;

namespace Nowaste.Domain.Repositories.Order;

public interface IOrderUpdateOnlyRepository
{
    Task<OrderEntity?> GetById(Guid id);
    void Update(OrderEntity order);
}
