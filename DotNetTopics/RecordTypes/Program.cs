using RecordTypes.Models;

var person = new Person("Ivan Jevtic", new DateOnly(1995, 10, 16)); // properties that we define here is immutable
var person2 = person with
{
    FullName = "Branislav Jevtic"
};

var (name, dateOfBirth) = person; // name = Ivan Jevtic dateOfBirth = 10/16/1995


Console.WriteLine(person); // Person { FullName = Ivan Jevtic, DateOfBirth = 10/16/1995 }
Console.WriteLine(person == person2); // False

