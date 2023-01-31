using DomainDrivenDesign;
using DomainDrivenDesign.Entities;
using DomainDrivenDesign.ValueObjects;

var firstName = FirstName.Create("Ivan");

if (firstName.IsFaulted)
{
    Console.WriteLine(firstName.GetException().Message);
}

var account = new Account("email", AccountType.User, firstName.GetResult(), "Jevtic");

