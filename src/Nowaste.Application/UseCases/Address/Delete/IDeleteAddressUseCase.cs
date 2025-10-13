namespace Nowaste.Application.UseCases.Address.Delete;

public interface IDeleteAddressUseCase
{
    Task Execute(Guid id);
}
