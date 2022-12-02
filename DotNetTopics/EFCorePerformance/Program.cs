using Dapper;
using EFCorePerformance;
using EFCorePerformance.Entities;
using EFCorePerformance.MediatR.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Storage;
using System.Linq;
using System.Reflection;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddDbContext<DatabaseContext>(
    options => options.UseSqlServer(builder.Configuration.GetConnectionString("DefaultConnection")));
builder.Services.AddMediatR(Assembly.GetExecutingAssembly());

// Configure pipeline
var app = builder.Build();

/* Company and employee creating using one transaction */
app.MapPost("create-company", async (IMediator mediator) =>
{
    var company = new Company
    {
        Name = "Google"
    };

    try
    {
        await mediator.Send(new CreateCompanyAndAddEmployeesCommand
        {
            Company = company
        });

        return Results.Ok();
    }
    catch (Exception ex)
    {
        return Results.BadRequest(ex.Message);
    }
});

/* Update employee salaries - Naive approach (For each employee command will execute)*/
app.MapPut("update-salaries", async (int companyId, DatabaseContext dbContext) =>
{
    var company = await dbContext
        .Set<Company>()
        .Include(c => c.Employees)
        .FirstOrDefaultAsync(c => c.Id == companyId);

    if (company is null)
    {
        return Results.NotFound();
    }

    foreach (var employee in company.Employees)
    {
        employee.Salary *= 1.1m;
    }

    company.LastSalaryUpdateUtc = DateTime.UtcNow;

    await dbContext.SaveChangesAsync();

    return Results.Ok();
});

/*Sql one command save (much faster)*/
app.MapPut("update-salaries-sql", async (int companyId, DatabaseContext dbContext) =>
{
    await dbContext.Database.BeginTransactionAsync();
    await dbContext.Database.ExecuteSqlInterpolatedAsync(
        $"UPDATE dbo.Employees SET Salary = Salary * 1.1 WHERE CompanyId = {companyId}");

    var company = dbContext.Set<Company>()
                    .FirstOrDefault(c => c.Id == companyId);

    company.LastSalaryUpdateUtc = DateTime.UtcNow;

    await dbContext.SaveChangesAsync();
    await dbContext.Database.CommitTransactionAsync();

    return Results.Ok();
});

app.MapPut("update-salaries-dapper", async (int companyId, DatabaseContext dbContext) =>
{
    var transaction = await dbContext.Database.BeginTransactionAsync();

    await dbContext.Database.GetDbConnection().ExecuteAsync(
            "UPDATE dbo.Employees SET Salary = Salary * 1.1 WHERE CompanyId = @CompanyId",
            new { CompanyId = companyId },
            transaction.GetDbTransaction()
        );

    var company = dbContext.Set<Company>()
                    .FirstOrDefault(c => c.Id == companyId);

    company.LastSalaryUpdateUtc = DateTime.UtcNow;

    await dbContext.SaveChangesAsync();
    await dbContext.Database.CommitTransactionAsync();

    return Results.Ok();
});

app.UseHttpsRedirection();

app.Run();
