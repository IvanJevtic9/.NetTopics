namespace DomainDrivenDesign.Outbox
{
    public sealed class OutboxMessage
    {
        public Guid Id { get; set; }

        public string Type { get; set; } = string.Empty; // Type of domain event

        public string Content { get; set; } = string.Empty; // JSON content

        public DateTime OccuredOnUtc { get; set; }

        public DateTime? ProcecssedOnUtc { get; set; }

        public string? Error { get; set; }
    }
}
