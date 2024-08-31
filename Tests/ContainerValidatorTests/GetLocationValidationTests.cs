using Exceptions.Contracts.Services;
using FluentValidation;
using Moq;
using Services.Models.Request;
using Services.Validation;
using Xunit;

namespace Tests.ContainerValidatorTests;

public class GetLocationValidationTests
{
    private readonly ContainerValidator _validator;

    public GetLocationValidationTests()
    {
        _validator = new ContainerValidator(
            Provider.Get<IValidator<GetLocationModel>>(),
            new Mock<IValidator<GetContainersLocationModel>>().Object,
            new Mock<IValidator<GetContainersLocationByOrderIdModel>>().Object,
            new Mock<IValidator<UpdateLocationModel>>().Object,
            new Mock<IValidator<UpdateContainersLocationModel>>().Object);    
    }

    [Fact]
    public async Task ValidateAsync_Should_Be_Valid_With_Valid_Model()
    {
        // Arrange
        var model = new GetLocationModel { Id = Guid.NewGuid() };

        // Act
        var result = await _validator.ValidateAsync(model);

        // Assert
        Assert.True(result);
    }

    [Fact]
    public async Task ValidateAsync_Should_Throw_ServiceException_If_Id_Is_Invalid()
    {
        // Arrange
        var model = new GetLocationModel { Id = Guid.Empty };
        
        // Act
        
        // Assert
        await Assert.ThrowsAsync<ServiceException>(async () => 
            await _validator.ValidateAsync(model));
    } 
}