using Nowaste.Communication.Responses.Product;

namespace Nowaste.Application.UseCases.Product.GetAllByEstablishment;

public interface IGetAllProductsByEstablishmentUseCase {
    Task<ICollection<ResponseGetAllProductsByEstablishmentJson>> Execute(Guid establishmentId);
}
