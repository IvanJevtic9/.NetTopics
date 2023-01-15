namespace RecordTypes.Models
{
    //public record Person
    //{
    //    public string FullName { get; init; }
    //    public DateOnly DateOfBirth { get; init; }
    //}
    
    public record struct PersonAsStruct(string FullName, DateTime DateTime);

    public record Person(string FullName, DateOnly DateOfBirth);
}
