using Moq;
using Nowaste.Application.UseCases.Persons.Register;
using Nowaste.Communication.Requests.Persons;
using Nowaste.Communication.Responses.Persons;

namespace CommonTestUtilities.UseCases;
public class RegisterPersonUseCase {
    public static IRegisterPersonUseCase Build() {
        var mock = new Mock<IRegisterPersonUseCase>();

        mock.Setup(useCase => useCase.Execute(It.IsAny<RequestRegisterPersonJson>()))
            .ReturnsAsync((RequestRegisterPersonJson request) => {
                return new ResponseRegisteredPersonJson {
                    Fullname = request.FullName,
                };
            });

        return mock.Object;
    }
}
