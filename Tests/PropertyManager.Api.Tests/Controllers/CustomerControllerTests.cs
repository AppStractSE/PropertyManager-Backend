using Api.Controllers.v1;
using Api.Dto.Response.Customer.v1;
using Core.Domain;
using Core.Features.Queries.Customers;
using MapsterMapper;
using MediatR;
using Microsoft.Extensions.Logging;
using Moq;

namespace PropertyManager.Api.Tests.Controllers;

public class CustomerControllerTests
{
    [Fact]
    public async void GetAllCustomers_Should_ReturnCustomers_When_Sucessful()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<CustomerController>>();

        mockMapper.Setup(m => m.Map<Customer>(It.IsAny<Customer>())).Returns(new Customer());
            mockMapper.Setup(m => m.Map<CustomerResponseDto>(It.IsAny<Customer>())).Returns(new CustomerResponseDto()
            {
                Id = Guid.NewGuid(),
                Name = "Test Customer 1",
                AreaId = "Area 1",
                TeamId = "Team 1",
                Address = "Address 1",
                Slug = "Slug 1"
            });


        mockMediator.Setup(m => m.Send(It.IsAny<GetAllCustomersQuery>(), It.IsAny<CancellationToken>()))
            .ReturnsAsync(new List<Customer>
            {
                new Customer()
            });

        // Act
        var controller = new CustomerController(mockMediator.Object, mockMapper.Object, mockLogger.Object);
        var sut = await controller.GetAllCustomers();

        // Assert
        Assert.NotNull(sut);
        Assert.True(sut.Value.Count() > 0);
    }
}
