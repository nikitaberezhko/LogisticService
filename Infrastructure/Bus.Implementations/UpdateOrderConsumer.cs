using BusModels;
using Domain;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.Repositories.Interfaces;

namespace Infrastructure.Bus.Implementations;

public class UpdateOrderConsumer(
    IContainerRepository containerRepository,
    ILogger<UpdateOrderConsumer> logger) : IConsumer<OrderUpdatedMessage>
{
    public async Task Consume(ConsumeContext<OrderUpdatedMessage> context)
    {
        var containerIds = context.Message.ContainerIds;
        var orderId = context.Message.OrderId;
        
        await containerRepository.UpdateContainersAsync(
            containers: containerIds.Select(id => new Container { Id = id, OrderId = orderId }).ToList(),
            orderId: orderId);
        
        logger.LogInformation("\"Order updated message\" received for order with id: {id}", orderId);
    }
}