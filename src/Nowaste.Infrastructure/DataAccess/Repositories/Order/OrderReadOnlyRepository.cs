using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Order;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Order;

internal class OrderReadOnlyRepository(NowasteDbContext dbContext) : IOrderReadOnlyRepository
{
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<OrderEntity?> GetById(Guid id)
    {
        return await _dbContext
            .Orders.AsNoTracking()
            .Include(order => order.Establishment)
            .Include(order => order.Person)
            .Include(order => order.OrderItems)
            .ThenInclude(order => order.Product)
            .FirstOrDefaultAsync(order => order.Id.Equals(id));
    }

    public async Task<OrderItemEntity?> GetOrderItemById(Guid id)
    {
        return await _dbContext
            .OrderItems.AsNoTracking()
            .FirstOrDefaultAsync(orderItem => orderItem.Id.Equals(id));
    }

    public async Task<int> GetOrdersCountByEstablishment(Guid establishmentId)
    {
        return await _dbContext
            .Orders.AsNoTracking()
            .CountAsync(order => order.EstablishmentId.Equals(establishmentId));
    }
}
