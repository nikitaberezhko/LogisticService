using BusModels;
using Domain;
using HubService.Contracts.Request;
using Infrastructure.Refit.Clients;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.Repositories.Interfaces;

namespace Infrastructure.Bus.Implementations;

public class UpdateOrderConsumer(
    ILocationRepository locationRepository,
    IHubApi hubApi,
    ILogger<UpdateOrderConsumer> logger) : IConsumer<OrderUpdatedMessage>
{
    public async Task Consume(ConsumeContext<OrderUpdatedMessage> context)
    {
        var containerIds = context.Message.ContainerIds;
        var orderId = context.Message.OrderId;
        var newHubId = context.Message.HubStartId;
        
        var startLocation = 
            await hubApi.GetHubById(new GetHubByIdRequest { Id = newHubId });
        
        await locationRepository.UpdateContainersAsync(
            containers: containerIds.Select(id => new Container { 
                Id = id, 
                OrderId = orderId,
                Latitude = startLocation.Data!.Latitude,
                Longitude = startLocation.Data!.Longitude,
                LastUpdateTime = DateTime.UtcNow
            }).ToList(),
            orderId: orderId);
        
        logger.LogInformation("\"Order updated message\" received for order with id: {id}", orderId);
    }
}