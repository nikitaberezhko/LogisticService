using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.ContainerValidatorTests;

public class UpdateLocationValidationTests
{
    private readonly ContainerValidator _validator;

    public UpdateLocationValidationTests()
    {
        _validator = new ContainerValidator(
            new Mock<IValidator<GetLocationModel>>().Object,
            new Mock<IValidator<GetContainersLocationModel>>().Object,
            new Mock<IValidator<GetContainersByOrderIdModel>>().Object,
            Provider.Get<IValidator<UpdateLocationModel>>(),
            new Mock<IValidator<UpdateContainersLocationModel>>().Object);  
    }

    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var model = new UpdateLocationModel
        {
            Id = Guid.NewGuid(),
            Latitude = 1,
            Longitude = 1
        };
        
        // Act
        var result = await _validator.ValidateAsync(model);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Id_Is_Invalid()
    {
        // Arrange
        var model = new UpdateLocationModel
        {
            Id = Guid.Empty,
            Latitude = 1,
            Longitude = 1
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Latitude_Less_Than_Negative_90()
    {
        // Arrange
        var model = new UpdateLocationModel
        {
            Id = Guid.NewGuid(),
            Latitude = -91,
            Longitude = 1
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Latitude_Greater_Than_90()
    {
        // Arrange
        var model = new UpdateLocationModel
        {
            Id = Guid.NewGuid(),
            Latitude = 91,
            Longitude = 1
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Longitude_Less_Than_Negative_180()
    {
        // Arrange
        var model = new UpdateLocationModel
        {
            Id = Guid.NewGuid(),
            Latitude = 1,
            Longitude = -181
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Longitude_Greater_Than_180()
    {
        // Arrange
        var model = new UpdateLocationModel
        {
            Id = Guid.NewGuid(),
            Latitude = 1,
            Longitude = 181
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }
}