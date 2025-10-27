using BarberBoss.Application.UseCases.Billings.Delete;
using BarberBoss.Application.UseCases.Billings.GetAll;
using BarberBoss.Application.UseCases.Billings.GetById;
using BarberBoss.Application.UseCases.Billings.Register;
using BarberBoss.Application.UseCases.Billings.Update;
using BarberBoss.Application.UseCases.Reports.Get;
using Microsoft.Extensions.DependencyInjection;

namespace BarberBoss.Application;
public static class DependecyInjectionExtension
{
    public static void AddApplication(this IServiceCollection services)
    {
      AddUseCases(services);
    }

    private static void AddUseCases(this IServiceCollection services)
    {
        services.AddScoped<IRegisterBillingUseCase, RegisterBillingUseCase>();
        services.AddScoped<IGetAllBillingUseCase, GetAllBillingUseCase>();
        services.AddScoped<IGetBillingByIdUseCase, GetBillingByIdUseCase>();
        services.AddScoped<IUpdateBillingUseCase, UpdateBillingUseCase>();
        services.AddScoped<IDeleteBillingUseCase, DeleteBillingUseCase>();
        services.AddScoped<IGetReportExcelUseCase, GetReportExcelUseCase>();
        services.AddScoped<IGetReportPDFUseCase, GetReportPDFUseCase>();
    }
}
