using BusModels;
using Domain;
using MassTransit;
using Microsoft.Extensions.Logging;
using Services.Repositories.Interfaces;

namespace Infrastructure.Bus.Implementations;

public class DeleteOrderConsumer(
    IContainerRepository containerRepository,
    ILogger<DeleteOrderConsumer> logger) : IConsumer<OrderDeletedMessage>
{
    public async Task Consume(ConsumeContext<OrderDeletedMessage> context)
    {
        var containerIds = context.Message.ContainerIds;
        var orderId = context.Message.OrderId;
        
        await containerRepository.DeleteContainersAsync(
            containers: containerIds.Select(id => new Container { Id = id, OrderId = orderId }).ToList(),
            orderId: orderId);
        
        logger.LogInformation("\"Order deleted message\" received with containers: {ids}", 
            string.Join(", ", context.Message.ContainerIds));
    }
}