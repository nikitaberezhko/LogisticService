using Domain;
using Exceptions.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;

namespace Infrastructure.Repositories.Implementations;

public class ContainerRepository(DbContext context) : IContainerRepository
{
    public async Task<Container> GetContainerLocation(Container container)
    {
        var existingContainer = await context.Set<Container>()
            .FirstOrDefaultAsync(x => x.Id == container.Id);
        if(existingContainer != null)
            return existingContainer;

        throw new InfrastructureException
        {
            Title = "Container not found",
            Message = "Container with this id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public async Task<List<Container>> GetContainersListLocation(List<Container> containers)
    {
        var existingContainers = await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id)).ToListAsync();
        
        return existingContainers;
    }
    
    public async Task<List<Container>> GetContainersListByOrderId(Guid orderId)
    {
        var existingContainers = await context.Set<Container>()
            .Where(x => x.OrderId == orderId).ToListAsync();
        return existingContainers;
    }

    public async Task<Container> UpdateContainerLocation(Container container)
    {
        var existingContainer = await context.Set<Container>().FirstOrDefaultAsync(x => x.Id == container.Id);
        if (existingContainer != null)
        {
            await context.Set<Container>().Where(x => x.Id == container.Id)
                .ExecuteUpdateAsync(s => s
                    .SetProperty(p => p.Latitude, container.Latitude)
                    .SetProperty(p => p.Longitude, container.Longitude));
        }
        
        throw new InfrastructureException
        {
            Title = "Container not found",
            Message = "Container with this id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public async Task<List<Container>> UpdateContainersListLocation(List<Container> containers)
    {
        var existingContainers = await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id)).ToListAsync();
        if (existingContainers.Count == containers.Count)
        {
            UpdateLocationFields(containers, existingContainers);
            await context.SaveChangesAsync();
        }
        
        throw new InfrastructureException
        {
            Title = "One or more containers not found",
            Message = "One or more containers with this id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public async Task CreateContainersAsync(List<Container> containers, Guid orderId)
    {
        var existingContainers = await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id) || x.OrderId == orderId).ToListAsync();
        if (existingContainers.Count != 0)
            context.RemoveRange(existingContainers);
        
        await context.Set<Container>().AddRangeAsync(containers);
        await context.SaveChangesAsync();
    }

    public async Task UpdateContainersAsync(List<Container> containers, Guid orderId)
    {
        var uselessContainers = await context.Set<Container>()
            .Where(x => !containers.Select(e => e.Id).Contains(x.Id) && x.OrderId == orderId).ToListAsync();
        if (uselessContainers.Count != 0)
            context.RemoveRange(uselessContainers);

        var existingContainers = await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id) || x.OrderId == orderId).ToListAsync();
        var newContainers = existingContainers.Where(x => !containers.Select(e => e.Id).Contains(x.Id)).ToList();
        await context.Set<Container>().AddRangeAsync(newContainers);
        await context.SaveChangesAsync();
    }

    public async Task DeleteContainersAsync(List<Container> containers, Guid orderId)
    {
        var existingContainers = await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id) || x.OrderId == orderId).ToListAsync();
        if (existingContainers.Count != 0)
            context.RemoveRange(existingContainers);
    }
    
    
    
    private void UpdateLocationFields(List<Container> containers, List<Container> containersInDb)
    {
        foreach (var containerInDb in containersInDb)
        {
            containerInDb.Latitude = containers.FirstOrDefault(x => x.Id == containerInDb.Id)!.Latitude;
            containerInDb.Longitude = containers.FirstOrDefault(x => x.Id == containerInDb.Id)!.Longitude;
            containerInDb.LastUpdateTime = DateTime.UtcNow;
        }
    }
}