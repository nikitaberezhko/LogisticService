using AutoMapper;
using Domain;
using Services.Models.Request;
using Services.Models.Response;
using Services.Repositories.Interfaces;
using Services.Validation;

namespace Services.Services.Implementations;

public class ContainerService(
    IMapper mapper,
    IContainerRepository containerRepository,
    ContainerValidator validator)
{
    public async Task<ContainerModel> GetLocation(GetContainerLocationModel model)
    {
        await validator.ValidateAsync(model);
        
        var container = await containerRepository
            .GetLocationAsync(mapper.Map<Container>(model));
        var result = mapper.Map<ContainerModel>(container);

        return result;
    }

    public async Task<List<ContainerModel>> GetContainersLocation(
        GetContainersListLocationModel model)
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
        GetContainersListByOrderIdModel model)
    {
        await validator.ValidateAsync(model);
        
        var containers = await containerRepository
            .GetContainersLocationByOrderIdAsync(model.OrderId);
        var result = mapper.Map<List<ContainerModel>>(containers);
        
        return result;
    }

    public async Task<ContainerModel> UpdateLocation(UpdateContainerLocationModel model)
    {
        await validator.ValidateAsync(model);

        var container = await containerRepository
            .UpdateLocationAsync(mapper.Map<Container>(model));
        var result = mapper.Map<ContainerModel>(container);

        return result;
    }

    public async Task<List<ContainerModel>> UpdateContainersLocation(
        UpdateContainersListLocationModel model)
    {
        await validator.ValidateAsync(model);

        var containers = await containerRepository
            .UpdateContainersLocationAsync(mapper.Map<List<Container>>(model.ContainersList));
        var result = mapper.Map<List<ContainerModel>>(containers);
        
        return result;
    }
}