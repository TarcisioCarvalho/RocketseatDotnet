using BarberBoss.Communication.Requests;

namespace BarberBoss.Application.UseCases.Reports.Get;
public interface IGetReportPDFUseCase
{
    Task<byte[]> Execute(RequestReportJson request);
}
