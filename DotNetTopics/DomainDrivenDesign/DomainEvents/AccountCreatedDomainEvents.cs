using DomainDrivenDesign.Primitives;

namespace DomainDrivenDesign.DomainEvents
{
    public sealed record AccountCreatedDomainEvents(Guid AccountId) : IDomainEvent
    {
    }
}
