using BusModels;
using Domain;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.Repositories.Interfaces;

namespace Infrastructure.Bus.Implementations;

public class CreateOrderConsumer(
    IContainerRepository containerRepository,
    ILogger<CreateOrderConsumer> logger) : IConsumer<OrderCreatedMessage>
{
    public async Task Consume(ConsumeContext<OrderCreatedMessage> context)
    {
        var containerIds = context.Message.ContainerIds;
        var orderId = context.Message.OrderId;
        
        // todo: add refit call to hub microservice for install hub location
        
        await containerRepository.CreateContainersAsync(
            containers: containerIds.Select(id => new Container { Id = id, OrderId = orderId }).ToList(),
            orderId: orderId);
        
        logger.LogInformation("\"Order created message\" received for order with id: {id}", context.Message.OrderId);
    }
}