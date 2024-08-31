using AutoMapper;
using Domain;
using Services.Models.Request;
using Services.Models.Response;
using Services.Repositories.Interfaces;
using Services.Services.Interfaces;
using Services.Validation;

namespace Services.Services.Implementations;

public class ContainerService(
    IMapper mapper,
    IContainerRepository containerRepository,
    ContainerValidator validator) : IContainerService
{
    public async Task<ContainerModel> GetLocation(GetLocationModel model)
    {
        await validator.ValidateAsync(model);
        
        var container = await containerRepository
            .GetLocationAsync(mapper.Map<Container>(model));
        var result = mapper.Map<ContainerModel>(container);

        return result;
    }

    public async Task<List<ContainerModel>> GetContainersLocation(
        GetContainersLocationModel model)
    {
        await validator.ValidateAsync(model);
        
        var containers = await containerRepository
            .GetContainersLocationAsync(model.IdsList
                .Select(id => new Container{ Id = id })
                .ToList());
        var result = mapper.Map<List<ContainerModel>>(containers);
        
        return result;
    }

    public async Task<List<ContainerModel>> GetContainersLocationByOrderId(
        GetContainersByOrderIdModel model)
    {
        await validator.ValidateAsync(model);
        
        var containers = await containerRepository
            .GetContainersLocationByOrderIdAsync(model.OrderId);
        var result = mapper.Map<List<ContainerModel>>(containers);
        
        return result;
    }

    public async Task<ContainerModel> UpdateLocation(UpdateLocationModel model)
    {
        await validator.ValidateAsync(model);

        var container = await containerRepository
            .UpdateLocationAsync(mapper.Map<Container>(model));
        var result = mapper.Map<ContainerModel>(container);

        return result;
    }

    public async Task<List<ContainerModel>> UpdateContainersLocation(
        UpdateContainersLocationModel model)
    {
        await validator.ValidateAsync(model);

        var containers = await containerRepository
            .UpdateContainersLocationAsync(mapper.Map<List<Container>>(model.ContainersList));
        var result = mapper.Map<List<ContainerModel>>(containers);
        
        return result;
    }
}