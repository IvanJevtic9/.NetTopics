using EFCorePerformance.Entities;
using MediatR;

namespace EFCorePerformance.MediatR.Commands
{
    public class CreateCompanyAndAddEmployeesCommand : IRequest<Unit>
    {
        public Company Company { get; set; }
    }
}
