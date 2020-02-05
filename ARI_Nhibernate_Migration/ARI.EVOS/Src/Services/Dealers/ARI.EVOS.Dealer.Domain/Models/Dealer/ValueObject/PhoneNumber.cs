using System;
using System.Text.RegularExpressions;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store phone number value
    /// </summary>
    public class PhoneNumber : ValueObject<PhoneNumber>
    {
        public virtual string Phone { get; protected set; }
        public CountryCode CountryCode { get; protected set; }

        protected PhoneNumber()
        {
        }

        private PhoneNumber(string phone, CountryCode countryCode)
        {
            Phone = phone;
            CountryCode = countryCode;
        }

        public static PhoneNumber Create(string phone, CountryCode countryCode)
        {
            var phoneNumber = new PhoneNumber(phone, countryCode);
            return phoneNumber.IsValid() ? phoneNumber : throw new Exception();
        }
        
        private bool IsValid()
        {
            if (!string.IsNullOrEmpty(Phone))
            {
                //If Country is UK and phone number has not valid length to 25 or 10.
                if (CountryCode.Code == "UK")
                {
                    if (Phone.Length != 25)
                    {
                        throw new Exception(DealerDomainConstant.PhoneUkValidation);
                    }
                    else if (Phone.Length > 0 && Phone.Length != 10)
                    {
                        throw new Exception(DealerDomainConstant.PhoneInvalid);
                    }
                }

                //If country is not UK and phone number has not valid length to 15 with proper format of &&&-&&&-&&&&
                Match match = Regex.Match(Phone, "^([0-9]{3}-)+([0-9]{3}-)+[0-9]{4}$");
                if (CountryCode.Code != "UK" && Phone.Length != 12)
                {
                    throw new Exception(DealerDomainConstant.PhoneNotUkValidation);
                }
                else if (CountryCode.Code != "UK" && Phone.Length == 12 && !match.Success)
                {
                    throw new Exception(DealerDomainConstant.PhoneNotUkWithFormat);
                }
            }

            return true;
        }

        protected override bool EqualsCore(PhoneNumber phoneNumber)
        {
            return Phone == phoneNumber.Phone && CountryCode == phoneNumber.CountryCode;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}
