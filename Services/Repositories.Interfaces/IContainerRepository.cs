using Domain;

namespace Services.Repositories.Interfaces;

public interface IContainerRepository
{
    Task<Container> GetContainerLocation(Container container);

    Task<List<Container>> GetContainersListLocation(List<Container> containers);

    Task<Container> UpdateContainerLocation(Container container);

    Task<List<Container>> UpdateContainersListLocation(List<Container> containers);

    Task CreateContainersAsync(List<Container> containers, Guid orderId);

    Task UpdateContainersAsync(List<Container> containers, Guid orderId);
    
    Task DeleteContainersAsync(List<Container> containers, Guid orderId);
}