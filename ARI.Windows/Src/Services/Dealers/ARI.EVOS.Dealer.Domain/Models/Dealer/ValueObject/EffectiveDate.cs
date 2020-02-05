using Chassis.Domain.Aggregate;
using System;
using System.Globalization;

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
                    var date = DateTime.Parse(Date.ToString()).ToString("MM'/'dd'/'yyyy");
                    Date = DateTime.ParseExact(date, "MM'/'dd'/'yyyy", CultureInfo.InvariantCulture, DateTimeStyles.None);

                }
                else
                {
                    return false;
                }
            }

            return true;
        }
        protected override bool EqualsCore(EffectiveDate other)
        {
            return Date == other.Date;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}
