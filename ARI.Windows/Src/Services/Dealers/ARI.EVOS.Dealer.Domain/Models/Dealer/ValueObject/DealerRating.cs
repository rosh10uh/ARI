using Chassis.Domain.Aggregate;

namespace ARI.EVOS.Dealer.Domain.Models.Dealer.ValueObject
{
    /// <summary>
    /// This class is used to store dealer rating value
    /// </summary>
    public class DealerRating : ValueObject<DealerRating>
    {
        public virtual string Rating { get; protected set; }

        protected DealerRating()
        {
        }

        private DealerRating(string rating)
        {
            Rating = rating;
        }

        public static DealerRating Create(string rating)
        {
            return new DealerRating(rating);
        }

        protected override bool EqualsCore(DealerRating other)
        {
            return Rating == other.Rating;
        }

        protected override int GetHashCodeCore()
        {
            return base.GetHashCode();
        }
    }
}
