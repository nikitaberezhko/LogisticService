using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.OtherModels;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.ContainerValidatorTests;

public class UpdateContainersLocationValidationTests
{
    private readonly ContainerValidator _validator;

    public UpdateContainersLocationValidationTests()
    {
        _validator = new ContainerValidator(
            new Mock<IValidator<GetLocationModel>>().Object,
            new Mock<IValidator<GetContainersLocationModel>>().Object,
            new Mock<IValidator<GetContainersByOrderIdModel>>().Object,
            new Mock<IValidator<UpdateLocationModel>>().Object,
            Provider.Get<IValidator<UpdateContainersLocationModel>>());  
    }

    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var model = new UpdateContainersLocationModel
        {
            ContainersList = [
                new ContainerUpdateModel { Id = Guid.NewGuid(), Latitude = 1, Longitude = 1 },
                new ContainerUpdateModel { Id = Guid.NewGuid(), Latitude = 1, Longitude = 1 }
            ]
        };
        
        // Act
        var result = await _validator.ValidateAsync(model);
        
        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_ContainersList_Is_Empty()
    {
        // Arrange
        var model = new UpdateContainersLocationModel
        {
            ContainersList = []
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_At_Least_One_Id_Is_Invalid()
    {
        // Arrange
        var model = new UpdateContainersLocationModel
        {
            ContainersList = [
                new ContainerUpdateModel { Id = Guid.Empty, Latitude = 1, Longitude = 1 },
                new ContainerUpdateModel { Id = Guid.NewGuid(), Latitude = 1, Longitude = 1 }
            ]
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_At_Least_One_Id_Latitude_Less_Than_Negative_90()
    {
        // Arrange
        var model = new UpdateContainersLocationModel
        {
            ContainersList = [
                new ContainerUpdateModel { Id = Guid.NewGuid(), Latitude = -91, Longitude = 1 },
                new ContainerUpdateModel { Id = Guid.NewGuid(), Latitude = 1, Longitude = 1 }
            ]
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_At_Least_One_Id_Latitude_Greater_Than_90()
    {
        // Arrange
        var model = new UpdateContainersLocationModel
        {
            ContainersList = [
                new ContainerUpdateModel { Id = Guid.NewGuid(), Latitude = 91, Longitude = 1 },
                new ContainerUpdateModel { Id = Guid.NewGuid(), Latitude = 1, Longitude = 1 }
            ]
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_At_Least_One_Id_Longitude_Less_Than_Negative_180()
    {
        // Arrange
        var model = new UpdateContainersLocationModel
        {
            ContainersList = [
                new ContainerUpdateModel { Id = Guid.NewGuid(), Latitude = 1, Longitude = -181 },
                new ContainerUpdateModel { Id = Guid.NewGuid(), Latitude = 1, Longitude = 1 }
            ]
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_At_Least_One_Id_Longitude_Greater_Than_180()
    {
        // Arrange
        var model = new UpdateContainersLocationModel
        {
            ContainersList = [
                new ContainerUpdateModel { Id = Guid.NewGuid(), Latitude = 1, Longitude = 181 },
                new ContainerUpdateModel { Id = Guid.NewGuid(), Latitude = 1, Longitude = 1 }
            ]
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }
}