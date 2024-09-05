using AutoMapper;
using Domain;
using Services.Models.Request;
using Services.Models.Response;
using Services.Repositories.Interfaces;
using Services.Services.Interfaces;
using Services.Validation;

namespace Services.Services.Implementations;

public class LocationService(
    IMapper mapper,
    ILocationRepository locationRepository,
    ContainerValidator validator) : ILocationService
{
    public async Task<ContainerModel> GetLocation(GetLocationModel model)
    {
        await validator.ValidateAsync(model);
        
        var container = await locationRepository
            .GetLocationAsync(mapper.Map<Container>(model));
        var result = mapper.Map<ContainerModel>(container);

        return result;
    }

    public async Task<List<ContainerModel>> GetContainersLocation(
        GetContainersLocationModel model)
    {
        await validator.ValidateAsync(model);
        
        var containers = await locationRepository
            .GetContainersLocationAsync(model.IdsList
                .Select(id => new Container{ Id = id })
                .ToList());
        var result = mapper.Map<List<ContainerModel>>(containers);
        
        return result;
    }

    public async Task<List<ContainerModel>> GetContainersLocationByOrderId(
        GetContainersLocationByOrderIdModel model)
    {
        await validator.ValidateAsync(model);
        
        var containers = await locationRepository
            .GetContainersLocationByOrderIdAsync(model.OrderId);
        var result = mapper.Map<List<ContainerModel>>(containers);
        
        return result;
    }

    public async Task<List<ContainerModel>> UpdateContainersLocation(
        UpdateContainersLocationModel model)
    {
        await validator.ValidateAsync(model);

        var containers = await locationRepository
            .UpdateContainersLocationAsync(mapper.Map<List<Container>>(model.ContainersList));
        var result = mapper.Map<List<ContainerModel>>(containers);
        
        return result;
    }
}