using Exceptions.Contracts.Services;
using FluentValidation;
using Microsoft.AspNetCore.Http;
using Services.Models.Request;

namespace Services.Validation;

public class ContainerValidator(
    IValidator<GetLocationModel> getLocationValidator,
    IValidator<GetContainersLocationModel> getContainersLocationValidator,
    IValidator<GetContainersByOrderIdModel> getContainersByOrderIdValidator,
    IValidator<UpdateLocationModel> updateLocationValidator,
    IValidator<UpdateContainersLocationModel> updateContainersLocationValidator)
{
    public async Task<bool> ValidateAsync(GetLocationModel model)
    {
        var validationResult = await getLocationValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(GetContainersLocationModel model)
    {
        var validationResult = await getContainersLocationValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(GetContainersByOrderIdModel model)
    {
        var validationResult = await getContainersByOrderIdValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(UpdateLocationModel model)
    {
        var validationResult = await updateLocationValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }
    
    public async Task<bool> ValidateAsync(UpdateContainersLocationModel model)
    {
        var validationResult = await updateContainersLocationValidator.ValidateAsync(model);
        if (!validationResult.IsValid)
            ThrowWithStandartMessage();
        
        return true;
    }

    private void ThrowWithStandartMessage() =>
        throw new ServiceException
        {
            Title = "Model invalid",
            Message = "Model validation failed",
            StatusCode = StatusCodes.Status400BadRequest
        };
}