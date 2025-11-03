using Microsoft.Extensions.DependencyInjection;
using Nowaste.Application.AutoMapper;
using Nowaste.Application.UseCases.Address.Delete;
using Nowaste.Application.UseCases.Address.GetAllByEstablishment;
using Nowaste.Application.UseCases.Address.GetAllByInstitution;
using Nowaste.Application.UseCases.Address.GetAllByPerson;
using Nowaste.Application.UseCases.Address.GetById;
using Nowaste.Application.UseCases.Address.Register;
using Nowaste.Application.UseCases.Address.UpdateByEstablishment;
using Nowaste.Application.UseCases.Address.UpdateByInstitution;
using Nowaste.Application.UseCases.Address.UpdateByPerson;
using Nowaste.Application.UseCases.Auth.Login;
using Nowaste.Application.UseCases.Establishment.GetAllAvailableForAddress;
using Nowaste.Application.UseCases.Establishment.GetAllReviews;
using Nowaste.Application.UseCases.Establishment.GetById;
using Nowaste.Application.UseCases.Establishment.Register;
using Nowaste.Application.UseCases.Establishment.RegisterOperatingDay;
using Nowaste.Application.UseCases.Establishment.Stats;
using Nowaste.Application.UseCases.Establishment.Update;
using Nowaste.Application.UseCases.Establishment.UpdateOperatingDay;
using Nowaste.Application.UseCases.Establishment.VinculateEmployee;
using Nowaste.Application.UseCases.Order.Checkout;
using Nowaste.Application.UseCases.Order.ConfirmPayment;
using Nowaste.Application.UseCases.Order.GetAllByEstablishment;
using Nowaste.Application.UseCases.Order.GetAllByPerson;
using Nowaste.Application.UseCases.Order.GetById;
using Nowaste.Application.UseCases.Order.GetByPaymentSessionId;
using Nowaste.Application.UseCases.Order.Register;
using Nowaste.Application.UseCases.Product.GetAllByEstablishment;
using Nowaste.Application.UseCases.Product.GetAllCategories;
using Nowaste.Application.UseCases.Product.GetById;
using Nowaste.Application.UseCases.Product.Register;
using Nowaste.Application.UseCases.Product.RegisterCategory;
using Nowaste.Application.UseCases.Product.ToggleIsActive;
using Nowaste.Application.UseCases.Product.Update;
using Nowaste.Application.UseCases.Product.UpdatePrice;
using Nowaste.Application.UseCases.Review.Register;
using Nowaste.Application.UseCases.Review.RegisterEstablishmentResponse;
using Nowaste.Application.UseCases.Users.ChangePassword;
using Nowaste.Application.UseCases.Users.Register;
using Nowaste.Application.UseCases.Users.UpdateProfile;

namespace Nowaste.Application;

public static class DependencyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
        AddUseCases(services);
        AddAutoMapper(services);
    }

    public static void AddAutoMapper(IServiceCollection services)
    {
        services.AddAutoMapper(cfg => cfg.AddProfile<AutoMapping>());
    }

    public static void AddUseCases(IServiceCollection services)
    {
        services.AddScoped<ILoginUseCase, LoginUseCase>();

        services.AddScoped<IRegisterUserUseCase, RegisterUserUseCase>();
        services.AddScoped<IUpdateUserProfileUseCase, UpdateUserProfileUseCase>();
        services.AddScoped<IChangePasswordUseCase, ChangePasswordUseCase>();

        services.AddScoped<IRegisterAddressUseCase, RegisterAddressUseCase>();
        services.AddScoped<IGetAddressByIdUseCase, GetAddressByIdUseCase>();
        services.AddScoped<
            IGetAllAddressesByEstablishmentUseCase,
            GetAllAddressesByEstablishmentUseCase
        >();
        services.AddScoped<
            IGetAllAddressesByInstitutionUseCase,
            GetAllAddressesByInstitutionUseCase
        >();
        services.AddScoped<IGetAllAddressesByPersonUseCase, GetAllAddressesByPersonUseCase>();
        services.AddScoped<
            IUpdateAddressByEstablishmentUseCase,
            UpdateAddressByEstablishmentUseCase
        >();
        services.AddScoped<IUpdateAddressByInstitutionUseCase, UpdateAddressByInstitutionUseCase>();
        services.AddScoped<IUpdateAddressByPersonUseCase, UpdateAddressByPersonUseCase>();
        services.AddScoped<IDeleteAddressUseCase, DeleteAddressUseCase>();

        services.AddScoped<IRegisterEstablishmentUseCase, RegisterEstablishmentUseCase>();
        services.AddScoped<IRegisterOperatingDayUseCase, RegisterOperatingDayUseCase>();
        services.AddScoped<IUpdateOperatingDayUseCase, UpdateOperatingDayUseCase>();
        services.AddScoped<IGetEstablishmentByIdUseCase, GetEstablishmentByIdUseCase>();
        services.AddScoped<IGetAllEstablishmentReviewsUseCase, GetAllEstablishmentReviewsUseCase>();
        services.AddScoped<IEstablishmentStatsUseCase, EstablishmentStatsUseCase>();
        services.AddScoped<
            IGetAvailableEstablishmentForAddressUseCase,
            GetAllAvailableEstablishmentForAddressUseCase
        >();
        services.AddScoped<
            IVinculateEmployeeToEstablishmentUseCase,
            VinculateEmployeeToEstablishmentUseCase
        >();
        services.AddScoped<IUpdateEstablishmentUseCase, UpdateEstablishmentUseCase>();

        services.AddScoped<IRegisterProductUseCase, RegisterProductUseCase>();
        services.AddScoped<IRegisterProductCategoryUseCase, RegisterProductCategoryUseCase>();
        services.AddScoped<IGetProductByIdUseCase, GetProductByIdUseCase>();
        services.AddScoped<IGetAllProductCategoriesUseCase, GetAllProductCategoriesUseCase>();
        services.AddScoped<
            IGetAllProductsByEstablishmentUseCase,
            GetAllProductsByEstablishmentUseCase
        >();
        services.AddScoped<IToggleIsProductActiveUseCase, ToggleIsProductActiveUseCase>();
        services.AddScoped<IUpdateProductUseCase, UpdateProductUseCase>();
        services.AddScoped<IUpdateProductPriceUseCase, UpdateProductPriceUseCase>();

        services.AddScoped<IRegisterOrderUseCase, RegisterOrderUseCase>();
        services.AddScoped<IOrderCheckoutUseCase, OrderCheckoutUseCase>();
        services.AddScoped<
            IGetAllOrdersByEstablishmentUseCase,
            GetAllOrdersByEstablishmentUseCase
        >();
        services.AddScoped<IGetAllOrdersByPersonUseCase, GetAllOrdersByPersonUseCase>();
        services.AddScoped<IGetOrderByIdUseCase, GetOrderByIdUseCase>();
        services.AddScoped<IGetOrderByPaymentSessionIdUseCase, GetOrderByPaymentSessionIdUseCase>();
        services.AddScoped<IOrderConfirmPaymentUseCase, OrderConfirmPaymentUseCase>();

        services.AddScoped<IRegisterReviewUseCase, RegisterReviewUseCase>();
        services.AddScoped<
            IRegisterEstablishmentReviewResponseUseCase,
            RegisterEstablishmentReviewResponseUseCase
        >();
    }
}
