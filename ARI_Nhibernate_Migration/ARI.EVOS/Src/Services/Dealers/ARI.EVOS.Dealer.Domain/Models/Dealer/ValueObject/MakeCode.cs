using System;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store make code value
    /// </summary>
    public class MakeCode : ValueObject<MakeCode>
    {
        public virtual string Code { get; protected set; }
        public virtual CountryCode CountryCode { get; protected set; }

        protected MakeCode()
        {
        }

        private MakeCode(string code, CountryCode countryCode)
        {
            Code = code;
            CountryCode = countryCode;
        }

        public static MakeCode Create(string code, CountryCode countryCode)
        {
            var makeCode = new MakeCode(code, countryCode);
            return !makeCode.IsValid() ? makeCode : throw new Exception(DealerDomainConstant.MakeCodeBasedOnCountryCode);
        }

        private bool IsValid()
        {
            if (!string.IsNullOrEmpty(CountryCode.Code) && !string.IsNullOrEmpty(Code))
            {
                return CountryCode.Code == "US" && Code == "BL";
            }
            return false;
        }

        protected override bool EqualsCore(MakeCode makeCode)
        {
            return Code == makeCode.Code && CountryCode == makeCode.CountryCode;
        }

        protected override int GetHashCodeCore()
        {
            return 13 * Code.Length;
        }
    }
}
