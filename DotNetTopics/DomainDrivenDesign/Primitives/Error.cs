namespace DomainDrivenDesign.Primitives
{
    // One Example of model that we can use for validation. Custom Wrapper
    public class Error
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }

        public Error(string propertyName, string errorMessage)
        {
            if (propertyName is null)
            {
                throw new ArgumentNullException(nameof(propertyName));
            }

            if (errorMessage is null)
            {
                throw new ArgumentNullException(nameof(errorMessage));
            }

            PropertyName = propertyName;
            Message = errorMessage;
        }
    }
}
