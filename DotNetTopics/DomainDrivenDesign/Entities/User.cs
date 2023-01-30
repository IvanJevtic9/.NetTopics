using DomainDrivenDesign.Primitives;
using DomainDrivenDesign.ValueObjects;

namespace DomainDrivenDesign.Entities
{
    public sealed class User : Entity
    {
        public Guid Id { get; private set; }
        public FirstName FirstName { get; private set; } // Example of value object usage
        public string LastName { get; private set; }
        public Guid AccountId { get; private set; }
        public Account Account { get; private set; }

        private User(FirstName firstName, string lastName, Guid accountId) : base(Guid.NewGuid())
        {
            FirstName = firstName;
            LastName = lastName;
            AccountId = accountId;
        }

        internal static User Create(FirstName firstName, string lastName, Guid accountId)
        {
            return new User(firstName, lastName, accountId);
        }
    }
}
