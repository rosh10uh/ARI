using AutoMapper;
using EMP.Management.Command.Commands;
using EMP.Management.Models;

namespace EMP.Management.Command.Mapper
{
    public class DeleteEmployeeMapping : Profile
    {
        public DeleteEmployeeMapping()
        {
            CreateMap<EmployeeModel, DeleteEmployeeCommand>();
        }
    }
}
