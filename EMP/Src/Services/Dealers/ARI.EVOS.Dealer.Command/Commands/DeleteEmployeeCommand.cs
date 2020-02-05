using Chassis.Command.Interfaces;

namespace EMP.Management.Command.Commands
{
    public class DeleteEmployeeCommand : ICommand<string>
    {
        public string CountryCode { get; private set; }
        public string EmployeeId { get; private set; }
    }
}
