using Services.Models.Request;
using Services.Models.Response;

namespace Services.Services.Interfaces;

public interface ILocationService
{
    Task<ContainerModel> GetLocation(GetLocationModel model);

    Task<List<ContainerModel>> GetContainersLocation(
        GetContainersLocationModel model);

    Task<List<ContainerModel>> GetContainersLocationByOrderId(
        GetContainersLocationByOrderIdModel model);

    Task<ContainerModel> UpdateLocation(UpdateLocationModel model);

    Task<List<ContainerModel>> UpdateContainersLocation(
        UpdateContainersLocationModel model);
}