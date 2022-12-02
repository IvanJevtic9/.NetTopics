using EFCorePerformance.Entities;
using EFCorePerformance.MediatR.Commands;
using MediatR;

namespace EFCorePerformance.MediatR.Handlers
{
    public class CreateEmployeesHandler : IRequestHandler<CreateEmployeesCommand, Unit>
    {
        private readonly DatabaseContext _dbContext;

        public CreateEmployeesHandler(DatabaseContext db)
        {
            _dbContext = db;
        }

        public async Task<Unit> Handle(CreateEmployeesCommand request, CancellationToken cancellationToken)
        {
            await _dbContext.Set<Employee>().AddRangeAsync(request.Employees);

            return Unit.Value;
        }
    }
}
