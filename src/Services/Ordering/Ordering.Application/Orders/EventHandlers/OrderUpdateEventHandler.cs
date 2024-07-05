
namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderUpdateEventHandler(ILogger<OrderUpdateEventHandler> logger) : INotificationHandler<OrderUpdatedEvent>
    {
        public Task Handle(OrderUpdatedEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
