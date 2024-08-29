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
        var result = await context.Set<Container>()
            .FirstOrDefaultAsync(x => x.Id == container.Id);
        if(result != null)
            return result;

        throw new InfrastructureException
        {
            Title = "Container not found",
            Message = "Container with this id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }

    public async Task<List<Container>> GetContainersListLocation(List<Container> containers)
    {
        var result = await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id)).ToListAsync();
        
        return result;
    }

    public async Task<Container> UpdateContainerLocation(Container container)
    {
        var result = await context.Set<Container>().FirstOrDefaultAsync(x => x.Id == container.Id);
        if (result != null)
        {
            // todo: add not updating fields
            context.Entry(result).CurrentValues.SetValues(container);
            await context.SaveChangesAsync();
            return result;
        }
        
        throw new InfrastructureException
        {
            Title = "Container not found",
            Message = "Container with this id not found",
            StatusCode = StatusCodes.Status404NotFound
        };
    }
    
    public async Task CreateOrUpdateContainersAsync(List<Container> containers, Guid orderId)
    {
        var existingContainers = await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id) || x.OrderId == orderId).ToListAsync();
        if (existingContainers.Count != 0)
            context.RemoveRange(existingContainers);
        await context.Set<Container>().AddRangeAsync(containers);
        await context.SaveChangesAsync();
        
        await context.Set<Container>().AddRangeAsync(containers);
        await context.SaveChangesAsync();
    }

    public async Task DeleteContainersAsync(List<Container> containers, Guid orderId)
    {
        var existingContainers = await context.Set<Container>()
            .Where(x => containers.Select(e => e.Id).Contains(x.Id) || x.OrderId == orderId).ToListAsync();
        if (existingContainers.Count != 0)
            context.RemoveRange(existingContainers);
    }
}