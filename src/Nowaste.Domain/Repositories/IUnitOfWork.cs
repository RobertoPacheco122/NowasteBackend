namespace Nowaste.Domain.Repositories;

public interface IUnitOfWork {
    Task Commit();
}