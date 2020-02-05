using FluentNHibernate.Mapping;

namespace EMP.Management.Infrastructure.Mappings
{
    public class EmployeeMapping : ClassMap<Domain.Models.Employee.Aggregate.Employee>
    {
        public EmployeeMapping()
        {
            // Set Employee Domain 
            Table("EMPLOYEE");
            CompositeId()
                .KeyReference(x => x.CountryCode, "COUNTRYCODE")
                .KeyReference(x => x.EmployeeId, "EMPLOYEEID");
            Map(x =>x.FirstName).Column("FIRSTNAME");
            Map(x =>x.LastName).Column("LASTNAME");

            //Set Address Domain
            Component(a => a.Address, b =>
            {
                b.Map(c => c.Address1).Column("ADDRESS1");
                b.Map(c => c.Address2).Column("ADDRESS2");
                b.Map(c => c.Address3).Column("ADDRESS3");
                b.Map(c => c.LandMark).Column("LANDMARK");
            });

            Component(a=>a.Email,b=>
              {
                  b.Map(c => c.EmailAddress).Column("EMAIL");
            });

            // Set Bank Domain
            Component(a => a.BankDetail, b =>
            {
                b.Map(c => c.BankName).Column("BANKNAME");
                b.Map(c => c.AccountNumber).Column("ACCOUNTNUMBER");
            });
        }
    }
}
