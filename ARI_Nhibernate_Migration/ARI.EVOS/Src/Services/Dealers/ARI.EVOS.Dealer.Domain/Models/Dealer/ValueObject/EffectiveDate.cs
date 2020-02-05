using System;
using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store effective date value
    /// </summary>
    public class EffectiveDate : ValueObject<EffectiveDate>
    {
        public virtual DateTime? Date { get; protected set; }

        protected EffectiveDate()
        {
        }

        private EffectiveDate(DateTime? date)
        {
            Date = date;
        }

        public static EffectiveDate Create(DateTime? date)
        {
            var effectiveDate = new EffectiveDate(date);
            return effectiveDate.IsValid() ? effectiveDate : throw new Exception(DealerDomainConstant.EffectDateValidateErrorMessage);
        }
        
        private bool IsValid()
        {
            DateTime effectiveDate;

            if (Date != null)
            {
                if (DateTime.TryParse(Date.ToString(), out effectiveDate))
                {
                    String.Format("{0:MM/dd/yyyy}", Date);
                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        protected override bool EqualsCore(EffectiveDate effectiveDate)
        {
            return Date == effectiveDate.Date;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}
