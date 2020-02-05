using AutoMapper;
using EMP.Management.Command.Commands;
using EMP.Management.Models;

namespace EMP.Management.Command.Mapper
{
    public class UpdateEmployeeMapping : Profile
    {
        public UpdateEmployeeMapping()
        {
            CreateMap<EmployeeModel, UpdateEmployeeCommand>();
        }
    }
}
