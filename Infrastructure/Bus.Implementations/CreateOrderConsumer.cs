using BusModels;
using Domain;
using HubService.Contracts.Request;
using Infrastructure.Refit.Clients;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.Repositories.Interfaces;

namespace Infrastructure.Bus.Implementations;

public class CreateOrderConsumer(
    ILocationRepository locationRepository,
    IHubApi hubApi,
    ILogger<CreateOrderConsumer> logger) : IConsumer<OrderCreatedMessage>
{
    public async Task Consume(ConsumeContext<OrderCreatedMessage> context)
    {
        var containerIds = context.Message.ContainerIds;
        var orderId = context.Message.OrderId;
        var hubStartId = context.Message.HubStartId;
        
        var startLocation = 
            await hubApi.GetHubById(new GetHubByIdRequest { Id = hubStartId });
        
        await locationRepository.CreateContainersAsync(
            containers: containerIds.Select(id => new Container
            {
                Id = id, 
                OrderId = orderId, 
                Latitude = startLocation.Data!.Latitude, 
                Longitude = startLocation.Data!.Longitude,
                LastUpdateTime = DateTime.UtcNow
            }).ToList(),
            orderId: orderId);
        
        logger.LogInformation("\"Order created message\" received for order with id: {id}", context.Message.OrderId);
    }
}