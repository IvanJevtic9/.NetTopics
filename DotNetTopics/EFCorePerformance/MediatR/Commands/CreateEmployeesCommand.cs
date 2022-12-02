using EFCorePerformance.Entities;
using MediatR;

namespace EFCorePerformance.MediatR.Commands
{
    public class CreateEmployeesCommand : IRequest<Unit>
    {
        public List<Employee> Employees { get; set; }
    }
}
