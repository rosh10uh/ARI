using Chassis.Domain.Aggregate;
using System;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store bac code value
    /// </summary>
    public class BacCode : ValueObject<BacCode>
    {
        public virtual string Bac { get; protected set; }
        public virtual MakeCode MakeCode { get; protected set; }
        protected BacCode()
        {
        }

        private BacCode(string bAc, MakeCode makeCode)
        {
            Bac = bAc;
            MakeCode = makeCode;
        }

        public static BacCode Create(string bAc, MakeCode makeCode)
        {
            var bacCode = new BacCode(bAc, makeCode);
            return bacCode.IsValid() ? bacCode : throw new Exception();
        }

        /// <summary>
        /// BAC code must be enter only for the make model "CH", "BU", "CA", "GM", "PO", "OL", "SA", "SB".
        /// </summary>
        /// <returns></returns>
        private bool IsValid()
        {
            if (string.IsNullOrEmpty(Bac) && (MakeCode.Code == "CH" || MakeCode.Code == "BU" || MakeCode.Code == "CA" || MakeCode.Code == "GM" ||
                MakeCode.Code == "PO" || MakeCode.Code == "OL" || MakeCode.Code == "SA" || MakeCode.Code == "SB"))
            {
                throw new ArgumentException(DealerDomainConstant.BacEmptyErrorMessage);
            }
            else if (!string.IsNullOrEmpty(Bac) && MakeCode.Code != "CH" && MakeCode.Code != "BU" && MakeCode.Code != "CA" && MakeCode.Code != "GM" &&
                MakeCode.Code != "PO" && MakeCode.Code != "OL" && MakeCode.Code != "SA" && MakeCode.Code != "SB")
            {
                throw new ArgumentException(DealerDomainConstant.BacNotEmptyErrorMessage);
            }

            return true;
        }

        protected override bool EqualsCore(BacCode other)
        {
            return Bac == other.Bac && MakeCode == other.MakeCode;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}
