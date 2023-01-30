using DomainDrivenDesign.Primitives;

namespace DomainDrivenDesign.Entities
{
    public sealed class Company : Entity
    {
        public string Name { get; private set; }
        public Guid AccountId { get; private set; }
        public Account Account { get; set; }

        private Company(string name, Guid accountId) : base(Guid.NewGuid())
        {
            Name = name;
        }

        internal static Company Create(string name, Guid accountId)
        {
            return new Company(name, accountId);
        }
    }
}
