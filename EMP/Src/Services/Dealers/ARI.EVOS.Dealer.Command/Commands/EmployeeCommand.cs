using Chassis.Command.Interfaces;

namespace EMP.Management.Command.Commands
{
    public class EmployeeCommand : ICommand<string>
    {
        public string EmployeeId { get; private set; }
        public string CountryCode { get; private set; }
        public string FirstName { get; private set; }
        public string Lastname { get; private set; }
        public string Email { get; private set; }
        public string Address1 { get; private set; }
        public string Address2 { get; private set; }
        public string Address3 { get; private set; }
        public string LandMark { get; private set; }
        public string BankName { get; private set; }
        public string AccountNumber { get; private set; }

        public EmployeeCommand()
        {

        }
    }
}
