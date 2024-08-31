using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.ContainerValidatorTests;

public class GetContainersByOrderIdValidationTests
{
    private readonly ContainerValidator _validator;

    public GetContainersByOrderIdValidationTests()
    {
        _validator = new ContainerValidator(
            new Mock<IValidator<GetLocationModel>>().Object,
            new Mock<IValidator<GetContainersLocationModel>>().Object,
            Provider.Get<IValidator<GetContainersByOrderIdModel>>(),
            new Mock<IValidator<UpdateLocationModel>>().Object,
            new Mock<IValidator<UpdateContainersLocationModel>>().Object);    
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var model = new GetContainersByOrderIdModel
        {
            OrderId = Guid.NewGuid()
        };
        
        // Act
        var result = await _validator.ValidateAsync(model);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_OrderId_Is_Invalid()
    {
        // Arrange
        var model = new GetContainersByOrderIdModel
        {
            OrderId = Guid.Empty
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }
}