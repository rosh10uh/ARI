using EMP.Management.Command.Commands;

namespace EMP.Management.Command.Interface
{
    public interface IEmployeeValidation
    {
        bool IsValid(EmployeeCommand command);
    }
}
