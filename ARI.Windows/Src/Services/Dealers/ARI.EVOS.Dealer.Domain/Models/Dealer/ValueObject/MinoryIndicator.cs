using Chassis.Domain.Aggregate;
using System;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store minory indicator value
    /// </summary>
    public class MinoryIndicator : ValueObject<MinoryIndicator>
    {
        public virtual string MinIndicator { get; protected set; }

        protected MinoryIndicator()
        {
        }

        private MinoryIndicator(string minIndicator)
        {
            MinIndicator = minIndicator;
        }

        public static MinoryIndicator Create(string minIndicator)
        {
            var minoryIndicator = new MinoryIndicator(minIndicator);
            return minoryIndicator.IsValid() ? minoryIndicator : throw new Exception(DealerDomainConstant.ErrMinorityIndicator);
        }
        private bool IsValid()
        {
            if (!string.IsNullOrEmpty(MinIndicator))
            {
                return MinIndicator.Length > 0 && (MinIndicator.ToUpper() == "Y" || MinIndicator.ToUpper() == "N");
            }
            return true;
        }

        protected override bool EqualsCore(MinoryIndicator other)
        {
            return MinIndicator == other.MinIndicator;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}
