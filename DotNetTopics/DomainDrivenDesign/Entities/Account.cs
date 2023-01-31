using DomainDrivenDesign.DomainEvents;
using DomainDrivenDesign.Exceptions;
using DomainDrivenDesign.Primitives;
using DomainDrivenDesign.ValueObjects;

namespace DomainDrivenDesign.Entities
{
    public enum AccountType
    {
        Company,
        User
    }

    public sealed class Account : AggregateRoot
    {
        public string Email { get; private set; }
        public AccountType Type { get; private set; }
        public User User { get; private set; }
        public Company Company { get; private set; }

        public Account(string email, AccountType accountType, FirstName firstName = null, string lastName = null, string companyName = null) : base(Guid.NewGuid())
        {
            Email = email;
            Type = accountType;

            if (accountType == AccountType.User)
            {
                if (firstName is null)
                {
                    throw new RequiredFieldDomainException($"{nameof(firstName)} can't be null.");
                }

                if (lastName is null)
                {
                    throw new RequiredFieldDomainException($"{nameof(lastName)} can't be null.");
                }

                User = User.Create(firstName, lastName, Id);
            }
            else
            {
                if (companyName is null)
                {
                    throw new RequiredFieldDomainException($"{nameof(companyName)} can't be null.");
                }

                Company = Company.Create(companyName, Id);
            }

            RaiseDomainEvent(new AccountCreatedDomainEvents(Id));
        }
    }
}
