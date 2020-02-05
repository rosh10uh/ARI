using System;
using Chassis.Domain.Aggregate;

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
        
        private bool IsValid()
        {
            if (string.IsNullOrEmpty(Bac) && (MakeCode.Code == "GM" || MakeCode.Code == "CH" || MakeCode.Code == "BU" || MakeCode.Code == "CA" || MakeCode.Code == "OL" || MakeCode.Code == "SA" || MakeCode.Code == "SB"))
            {
                throw new Exception(DealerDomainConstant.BacEmptyErrorMessage);
            }

            if (!string.IsNullOrEmpty(Bac) && (MakeCode.Code != "GM" || MakeCode.Code != "CH" || MakeCode.Code != "BU" || MakeCode.Code != "CA" || MakeCode.Code != "OL" || MakeCode.Code != "SA" || MakeCode.Code != "SB"))
            {
                throw new Exception(DealerDomainConstant.BacNotEmptyErrorMessage);
            }

            return true;
        } 

        protected override bool EqualsCore(BacCode bAcCode)
        {
            return Bac == bAcCode.Bac && MakeCode == bAcCode.MakeCode;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}
