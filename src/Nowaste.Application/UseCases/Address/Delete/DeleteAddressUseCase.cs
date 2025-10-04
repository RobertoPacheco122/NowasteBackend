using Nowaste.Domain.Repositories;
using Nowaste.Domain.Repositories.Address;
using Nowaste.Exception.ExceptionBase;

namespace Nowaste.Application.UseCases.Address.Delete;

public class DeleteAddressUseCase(
        IUnitOfWork unitOfWork,
        IAddressUpdateOnlyRepository addressUpdateOnlyRepository
    ) : IDeleteAddressUseCase {
    private readonly IUnitOfWork _unitOfWork = unitOfWork;
    private readonly IAddressUpdateOnlyRepository _addressUpdateOnlyRepository = addressUpdateOnlyRepository;

    public async Task Execute(Guid id) {
        var addressEntity = await _addressUpdateOnlyRepository.GetById(id) ??
            throw new NotFoundException("Address not found");

        addressEntity.IsDeleted = true;
        addressEntity.UpdatedAt = DateTime.UtcNow;

        _addressUpdateOnlyRepository.Update(addressEntity);

        await _unitOfWork.Commit();
    }
}
