using DomainDrivenDesign.Primitives;
using MediatR;

namespace DomainDrivenDesign.Idempotence
{
    public sealed class IdempotentDomainEventHandler<TDomainEvent>//: IDomainEventHandler<TDomainEvent>
        where TDomainEvent : IDomainEvent
    {
        private readonly INotificationHandler<TDomainEvent> _decorated;
        // private 

        public IdempotentDomainEventHandler(INotificationHandler<TDomainEvent> decorated)
        {
            _decorated = decorated;
        }

        public async Task Handle(TDomainEvent notification, CancellationToken cancellationToken)
        {
            // check if there is a record for outboxMessage already stored in database, if exists just return, or Handle domain event

            await _decorated.Handle(notification, cancellationToken);

            // Save outboxMessageConsumer record into database , to indicate that this message is processed.
        }

        // Ova klasa se ponasa kao dekorator, wrapuje se INotificationHandler (originalni service) i dodaje se
        // funkcionalnost u njoj, svaki njen poziv ce se prebaciti na dekorator koji ima prosirenu logiku
        // builder.Service.Decorate(typeof(INotificationHandler<>), typeof(IdempotentDomainEventHandler<>)) 
    }
}
