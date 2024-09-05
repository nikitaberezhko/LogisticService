using Domain;
using Exceptions.Contracts.Infrastructure;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;
using Services.Repositories.Interfaces;

namespace Infrastructure.Repositories.Implementations;

public class LocationRepository(DbContext context) : ILocationRepository
{
    public async Task<Container> GetLocationAsync(Container container)
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

    public async Task<List<Container>> GetContainersLocationAsync(List<Container> containers)
    {
        var existingContainers = await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id)).ToListAsync();
        
        return existingContainers;
    }
    
    public async Task<List<Container>> GetContainersLocationByOrderIdAsync(Guid orderId)
    {
        var existingContainers = await context.Set<Container>()
            .Where(x => x.OrderId == orderId).ToListAsync();
        return existingContainers;
    }

    public async Task<List<Container>> UpdateContainersLocationAsync(List<Container> containers)
    {
        var existingContainers = await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id)).ToListAsync();
        if (existingContainers.Count == containers.Count)
        {
            UpdateLocationFields(containers, existingContainers);
            await context.SaveChangesAsync();
            
            return existingContainers;
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
        {
            context.RemoveRange(existingContainers);
            await context.SaveChangesAsync();
        }
        
        await context.Set<Container>().AddRangeAsync(containers);
        await context.SaveChangesAsync();
    }

    public async Task UpdateContainersAsync(List<Container> containers, Guid orderId, DateTime lastUpdateTime)
    {
        var uselessContainers = await context.Set<Container>()
            .Where(x => !containers.Select(e => e.Id).Contains(x.Id) && x.OrderId == orderId).ToListAsync();
        if (uselessContainers.Count != 0)
        {
            context.RemoveRange(uselessContainers);
            await context.SaveChangesAsync(); 
        }
        
        await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id))
            .ExecuteUpdateAsync(s => s
                .SetProperty(p => p.OrderId, orderId)
                .SetProperty(p => p.LastUpdateTime, lastUpdateTime));
        
        var newContainers = containers.Where(x => context.Set<Container>().Select(x => x.Id).Contains(x.Id) == false).ToList();
        await context.Set<Container>().AddRangeAsync(newContainers);
        await context.SaveChangesAsync();
    }

    public async Task DeleteContainersAsync(List<Container> containers, Guid orderId)
    {
        var existingContainers = await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id) || x.OrderId == orderId).ToListAsync();
        if (existingContainers.Count != 0)
        {
            context.RemoveRange(existingContainers);
            await context.SaveChangesAsync();
        }
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