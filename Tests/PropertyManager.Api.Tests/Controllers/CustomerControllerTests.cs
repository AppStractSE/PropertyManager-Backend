using Api.Controllers.v1;
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
    public void GetAllCustomers_Should_ReturnCustomers_When_Sucessful()
    {
        // Arrange
        var mockMediator = new Mock<IMediator>();
        var mockMapper = new Mock<IMapper>();
        var mockLogger = new Mock<ILogger<CustomerController>>();
        
        // Act
        var controller = new CustomerController(mockMediator.Object, mockMapper.Object, mockLogger.Object);
        var sut = controller.GetAllCustomers();

        // Assert
        Assert.NotNull(sut);
    }
}
