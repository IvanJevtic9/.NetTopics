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
        private Account(string email, AccountType type) : base(Guid.NewGuid())
        {
            Email = email;
            Type = type;
        }

        public static Account Create(string email, AccountType accountType, FirstName firstName = null, string lastName = null, string companyName = null)
        {
            var account = new Account(email, accountType);

            if (accountType == AccountType.User)
            {
                if(firstName is null)
                {
                    throw new RequiredFieldDomainException($"{nameof(firstName)} can't be null.");
                }

                if (lastName is null)
                {
                    throw new RequiredFieldDomainException($"{nameof(lastName)} can't be null.");
                }

                account.User = User.Create(firstName, lastName, account.Id);
            }
            else
            {
                if (companyName is null)
                {
                    throw new RequiredFieldDomainException($"{nameof(companyName)} can't be null.");
                }

                account.Company = Company.Create(companyName, account.Id);
            }

            return account;
        }
    }
}
