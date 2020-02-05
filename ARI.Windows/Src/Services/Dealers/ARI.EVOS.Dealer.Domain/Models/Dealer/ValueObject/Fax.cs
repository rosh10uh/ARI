using Chassis.Domain.Aggregate;
using System;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store fax value
    /// </summary>
    public class Fax : ValueObject<Fax>
    {
        public string FaxNumber { get; protected set; }
        public CountryCode CountryCode { get; protected set; }

        protected Fax()
        { }

        private Fax(string faxNumber, CountryCode countryCode)
        {
            FaxNumber = faxNumber;
            CountryCode = countryCode;
        }

        public static Fax Create(string faxNumber, CountryCode countryCode)
        {
            var Fax = new Fax(faxNumber, countryCode);
            return Fax.IsValid() ? Fax : throw new Exception();
        }

        private bool IsValid()
        {
            if (!string.IsNullOrEmpty(FaxNumber))
            {
                if (CountryCode.Code != "UK" && (FaxNumber.Length < 10 || FaxNumber.Length > 12))
                {
                    throw new ArgumentException(DealerDomainConstant.FaxValidateNotUk);
                }
                else if (CountryCode.Code == "UK" && FaxNumber.Length != 25)
                {
                    throw new ArgumentException(DealerDomainConstant.FaxValidateUk);
                }
            }
            return true;
        }
        protected override bool EqualsCore(Fax other)
        {
            return FaxNumber == other.FaxNumber && CountryCode == other.CountryCode;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}
