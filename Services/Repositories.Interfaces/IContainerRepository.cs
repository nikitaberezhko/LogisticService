using Domain;

namespace Services.Repositories.Interfaces;

public interface IContainerRepository
{
    Task<Container> GetLocationAsync(Container container);

    Task<List<Container>> GetContainersLocationAsync(List<Container> containers);

    Task<List<Container>> GetContainersLocationByOrderIdAsync(Guid orderId);

    Task<Container> UpdateLocationAsync(Container container);

    Task<List<Container>> UpdateContainersLocationAsync(List<Container> containers);

    Task CreateContainersAsync(List<Container> containers, Guid orderId);

    Task UpdateContainersAsync(List<Container> containers, Guid orderId);
    
    Task DeleteContainersAsync(List<Container> containers, Guid orderId);
}