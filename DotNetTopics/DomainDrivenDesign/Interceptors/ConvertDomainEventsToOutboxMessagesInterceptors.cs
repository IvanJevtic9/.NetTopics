using Polly;
using Polly.Retry;
using Newtonsoft.Json;
using DomainDrivenDesign.Outbox;
using DomainDrivenDesign.Primitives;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace DomainDrivenDesign.Interceptors
{
    // Have to register this as a Singleton
    // Also have to say dbContext to use this UseSqlServer(...).AddInterceptor(interceptor)
    public sealed class ConvertDomainEventsToOutboxMessagesInterceptors : SaveChangesInterceptor
    {
        public override async ValueTask<int> SavedChangesAsync(SaveChangesCompletedEventData eventData, int result, CancellationToken cancellationToken = default)
        {
            DbContext? dbContext = eventData.Context;

            if (dbContext is null)
            {
                return await base.SavedChangesAsync(eventData, result, cancellationToken);
            }

            // For every aggregate root that is changed in db context we want to tackle his occured domain events.
            var events = dbContext.ChangeTracker
                .Entries<AggregateRoot>()
                .Select(x => x.Entity)
                .SelectMany(aggregateRoot =>
                {
                    var domainEvents = aggregateRoot.GetDomainEvents(); // Get all occured events
                    aggregateRoot.ClearDomainEvents(); // removed events that is going to be processed

                    return domainEvents;
                })
                .Select(domainEvent => new OutboxMessage
                {
                    Id = Guid.NewGuid(),
                    OccuredOnUtc = DateTime.UtcNow,
                    Type = domainEvent.GetType().Name,
                    Content = JsonConvert.SerializeObject(
                        domainEvent,
                        new JsonSerializerSettings
                        {
                            TypeNameHandling = TypeNameHandling.All
                        })
                })
                .ToList();

            // save outbox messages to db. later some background task is going to process them (Publish, Notify to corresponding handler) 
            // dbContext.Set<OutbooxMessage>().AddRange(outboxMessages);

            return await base.SavedChangesAsync(eventData, result, cancellationToken);
        }


        // (Background task code for publishing saved domain events)
        // Implementing re-try mechanism for process outbox messages in background task

        // AsyncRetryPolicy policy = Policy
        //    .Handle<Exception>()
        //    .RetryAsync(3);
        // Another option WaitAndRetryAsync(3, attempt => TimeSpan.FromMilliseconds(50 * attempt)

        // take domain event from content in Outbox message and process it in ExecuteAndCaptureAsync method

        // PolicyResult policyResult = await policy.ExecuteAndCaptureAsync(() =>
        // {
        //  _publisher.Publish(domainEvent, context.CancellationToken));

        //  outboxMessage.Error = result.FinalException?.ToString();
        //  outboxMessage.ProcessOnUtc = DateTime.UtcNow;
        // });
    }
}
