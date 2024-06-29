
namespace Ordering.Domain.Abstractions
{
    public interface IAggragate<T> : IAggragate, IEntity<T>
    {

    }
    public interface IAggragate : IEntity
    {
        IReadOnlyList<IDomainEvent> DomainEvents { get; }
        IDomainEvent[] ClearDomainEvents(); 
    }
}
