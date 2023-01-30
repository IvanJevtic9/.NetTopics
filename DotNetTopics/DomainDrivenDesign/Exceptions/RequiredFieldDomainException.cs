namespace DomainDrivenDesign.Exceptions
{
    public sealed class RequiredFieldDomainException : DomainException
    {
        public RequiredFieldDomainException(string message) : base(message)
        { }
    }
}
