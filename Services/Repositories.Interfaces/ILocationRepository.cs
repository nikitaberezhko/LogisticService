using Domain;

namespace Services.Repositories.Interfaces;

public interface ILocationRepository
{
    Task<Container> GetLocationAsync(Container container);

    Task<List<Container>> GetContainersLocationAsync(List<Container> containers);

    Task<List<Container>> GetContainersLocationByOrderIdAsync(Guid orderId);

    Task<List<Container>> UpdateContainersLocationAsync(List<Container> containers);

    Task CreateContainersAsync(List<Container> containers, Guid orderId);

    Task UpdateContainersAsync(List<Container> containers, Guid orderId, DateTime lastUpdateTime);
    
    Task DeleteContainersAsync(List<Container> containers, Guid orderId);
}