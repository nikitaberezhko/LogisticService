using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.ContainerValidatorTests;

public class GetContainersLocationValidationTests
{
    private readonly ContainerValidator _validator;

    public GetContainersLocationValidationTests()
    {
        _validator = new ContainerValidator(
            new Mock<IValidator<GetLocationModel>>().Object,
            Provider.Get<IValidator<GetContainersLocationModel>>(),
            new Mock<IValidator<GetContainersLocationByOrderIdModel>>().Object,
            new Mock<IValidator<UpdateContainersLocationModel>>().Object);    
    }

    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var model = new GetContainersLocationModel
        {
            IdsList = [Guid.NewGuid(), Guid.NewGuid()]
        };
        
        // Act
        var result = await _validator.ValidateAsync(model);
        
        // Assert
        Assert.True(result);
    }
    
    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_At_Least_One_Id_Is_Invalid()
    {
        // Arrange
        var model = new GetContainersLocationModel
        {
            IdsList = [Guid.NewGuid(), Guid.Empty]
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_IdsList_Count_Less_1()
    {
        // Arrange
        var model = new GetContainersLocationModel
        {
            IdsList = []
        };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    } 
}