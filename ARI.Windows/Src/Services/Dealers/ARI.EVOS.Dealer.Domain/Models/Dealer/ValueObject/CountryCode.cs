﻿using Chassis.Domain.Aggregate;
using System;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store country code value
    /// </summary>
    public class CountryCode : ValueObject<CountryCode>
    {
        public virtual string Code { get; protected set; }

        protected CountryCode()
        {
        }

        private CountryCode(string code)
        {
            Code = code;
        }

        public static CountryCode Create(string code)
        {
            var countryCode = new CountryCode(code);
            return countryCode.IsValid() ? countryCode : throw new Exception(DealerDomainConstant.CountryCodeLengthErrorMessage);
        }

        private bool IsValid()
        {
            if (!string.IsNullOrEmpty(Code))
            {
                return Code.Length >= 2;
            }
            return true;
        }

        protected override bool EqualsCore(CountryCode other)
        {
            return Code == other.Code;
        }

        protected override int GetHashCodeCore()
        {
            return Code.Length;
        }
    }
}
