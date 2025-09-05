using Microsoft.EntityFrameworkCore;
using Nowaste.Domain.Entities;

namespace Nowaste.Infrastructure.DataAccess; 

public class NowasteDbContext(DbContextOptions options) : DbContext(options) {
    public DbSet<AddressEntity> Addresses { get; set; }
    public DbSet<EstablishmentEntity> Establishments { get; set; }
    public DbSet<InstitutionEntity> Institutions { get; set; }
    public DbSet<OrderEntity> Orders { get; set; }
    public DbSet<OrderItemEntity> OrderItems { get; set; }
    public DbSet<PersonEntity> Persons { get; set; }
    public DbSet<ProductCategoryEntity> ProductCategories { get; set; }
    public DbSet<ProductEntity> Products { get; set; }
    public DbSet<ProductPriceHistoryEntity> ProductPricesHistory { get; set; }
    public DbSet<ReviewEntity> Reviews { get; set; }
    public DbSet<UserEntity> Users { get; set; }
}
