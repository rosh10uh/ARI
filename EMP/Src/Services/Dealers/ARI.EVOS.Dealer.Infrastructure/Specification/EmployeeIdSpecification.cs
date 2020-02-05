using ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject;
using Chassis.Repository.Specification;
using EMP.Management.Domain.Models.Employee.ValueObject;
using System;
using System.Linq.Expressions;

namespace EMP.Management.Infrastructure.Specification
{
    public class EmployeeIdSpecification : Specification<Domain.Models.Employee.Aggregate.Employee>
    {
        private EmployeeId _employeeId;

        private CountryCode _countryCode;


        public EmployeeIdSpecification(EmployeeId employeeId, CountryCode countryCode)
        {
            _employeeId = employeeId;
            _countryCode = countryCode;
        }

        public override Expression<Func<Domain.Models.Employee.Aggregate.Employee, bool>> ToExpression()
        {
            return x => x.EmployeeId == _employeeId
                        && x.CountryCode == _countryCode;
        }
    }
}
