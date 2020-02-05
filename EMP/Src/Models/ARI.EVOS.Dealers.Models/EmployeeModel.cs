using Prism.Validation;

namespace EMP.Management.Models
{
    public class EmployeeModel :ValidatableBindableBase
    {
        public string EmployeeId { get; set; }
        public string CountryCode { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Address1 { get; set; }
        public string Address2 { get; set; }
        public string Address3 { get; set; }
        public string LandMark { get; set; }
        public string BankName { get; set; }
        public string AccountNumber { get; set; }
    }
}
