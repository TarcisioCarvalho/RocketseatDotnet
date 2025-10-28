using BarberBoss.Application.UseCases.Billings.GetAll;
using BarberBoss.Communication.Requests;
using BarberBoss.Domain.DTOs;
using BarberBoss.Domain.Enums;
using BarberBoss.Domain.Repositories;
using Moq;
using Xunit;

namespace BarberBoss.Tests.UseCases;
public class GetAllBillingUseCaseTest
{
    [Fact]
    public async Task Execute_ShouldReturnBillingsList()
    {
        var billings = new List<BillingShort>
        {
            new BillingShort { 
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow,
                Status = Status.Paid,
            },
            new BillingShort { 
                Id = Guid.NewGuid(),
                Date = DateTime.UtcNow.AddDays(-1),
                Status = Status.Paid,
            }
        };

        var billingRepositoryMock = new Mock<IBillingReadOnlyRepository>();

        billingRepositoryMock.Setup(repo => repo.GetAll(It.IsAny<int>(), It.IsAny<int>(), It.IsAny<DateTime?>(), It.IsAny<DateTime?>()))
            .ReturnsAsync((billings, 2, 200m));

        var useCase = new GetAllBillingUseCase(billingRepositoryMock.Object);
        var result = await useCase.Execute(new RequestBillingsJson
        {
            Page = 1,
            PageSize = 10,
            StartDate = null,
            EndDate = null
        });
        Assert.NotNull(result);
        Assert.Equal(2, result.Total);
        Assert.Equal(200m, result.TotalAmount);
        Assert.Equal(2, result.Billings.Count);
    }
}
