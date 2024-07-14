using MassTransit;
using Microsoft.FeatureManagement;

namespace Ordering.Application.Orders.EventHandlers.Domain
{
    public class OrderCreatedEventHandler(IPublishEndpoint publishEndPoint, IFeatureManager featureManager, ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreateEvent>
    {
        public async Task Handle(OrderCreateEvent domainEvent, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain event handled {DomainEvent}", domainEvent.GetType().Name);
            if (await featureManager.IsEnabledAsync("OrderFullfilment"))
            {
                var orderCreatedIngegrationEvent = domainEvent.order.ToOrderDto();
                await publishEndPoint.Publish(orderCreatedIngegrationEvent, cancellationToken);
            }
          
           
        }
    }
}
