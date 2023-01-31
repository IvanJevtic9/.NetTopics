using MediatR;
using DomainDrivenDesign.DomainEvents;

namespace DomainDrivenDesign.Events
{
    public class AccountCreatedDomainEventHandler : INotificationHandler<AccountCreatedDomainEvents>
    {
        public async Task Handle(AccountCreatedDomainEvents notification, CancellationToken cancellationToken)
        {
            // Send email notification about account
            Console.WriteLine($"Send activation mail for account {notification.AccountId}");  
            
        }
    }
}
