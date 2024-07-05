namespace Ordering.Application.Orders.EventHandlers
{
    public class OrderCreatedEventHandler(ILogger<OrderCreatedEventHandler> logger) : INotificationHandler<OrderCreateEvent>
    {
        public  Task Handle(OrderCreateEvent notification, CancellationToken cancellationToken)
        {
            logger.LogInformation("Domain event handled {DomainEvent}", notification.GetType().Name);
            return Task.CompletedTask;
        }
    }
}
