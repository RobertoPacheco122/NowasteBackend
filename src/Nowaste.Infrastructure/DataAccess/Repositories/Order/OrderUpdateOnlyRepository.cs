using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;
using Nowaste.Domain.Repositories.Order;

namespace Nowaste.Infrastructure.DataAccess.Repositories.Order;

internal class OrderUpdateOnlyRepository(NowasteDbContext dbContext) : IOrderUpdateOnlyRepository
{
    private readonly NowasteDbContext _dbContext = dbContext;

    public async Task<OrderEntity?> GetById(Guid id)
    {
        return await _dbContext
            .Orders.Include(order => order.Establishment)
            .Include(order => order.Person)
            .Include(order => order.OrderItems)
            .ThenInclude(order => order.Product)
            .FirstOrDefaultAsync(order => order.Id.Equals(id));
    }

    public void Update(OrderEntity order)
    {
        _dbContext.Orders.Update(order);
    }
}
