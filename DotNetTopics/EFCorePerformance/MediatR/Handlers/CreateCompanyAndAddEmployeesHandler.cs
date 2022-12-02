using EFCorePerformance.Entities;
using EFCorePerformance.MediatR.Commands;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace EFCorePerformance.MediatR.Handlers
{
    public class CreateCompanyAndAddEmployeesHandler : IRequestHandler<CreateCompanyAndAddEmployeesCommand, Unit>
    {
        private readonly IMediator _mediator;
        private readonly DatabaseContext _dbContext;

        public CreateCompanyAndAddEmployeesHandler(IMediator mediator, DatabaseContext db)
        {
            _mediator = mediator;
            _dbContext = db;
        }

        public async Task<Unit> Handle(CreateCompanyAndAddEmployeesCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.Database.BeginTransactionAsync(cancellationToken);

            //await _dbContext.Database.ExecuteSqlInterpolatedAsync(
            //    $"Insert into dbo.Companies (Name, LastSalaryUpdateUtc) values ({request.Company.Name}, null)");

            await _dbContext.Set<Company>().AddAsync(request.Company, cancellationToken);
            await _dbContext.SaveChangesAsync();

            var employees = Enumerable
                .Range(1, 5)
                .Select(id => new Employee
                {
                    Id = id,
                    CompanyId = request.Company.Id,
                    Name = $"New employee {id}",
                    Salary = 20
                })
                .ToList();

            await _mediator.Send(new CreateEmployeesCommand
            {
                Employees = employees
            });

            await _dbContext.Database.CommitTransactionAsync();

            return Unit.Value;
        }
    }
}
