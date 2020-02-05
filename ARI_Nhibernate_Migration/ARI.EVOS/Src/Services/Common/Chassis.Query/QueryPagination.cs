using System.Linq;

namespace Chassis.Query
{
    public abstract class QueryPagination
    {
        private int _perPage;
        private string _sort;

        public virtual int PerPage
        {
            get => (_perPage > 0 ? _perPage : 10);

            protected set => _perPage = value;
        }

        public virtual int Page { get; protected set; }

        public virtual string Sort
        {
            get => _sort;
            protected set
            {
                _sort = string.Join(',', value.Split(',').Select(x => x.Trim().StartsWith("-") ? string.Format("{0} {1} ", x.Replace("-", ""), "DESC") : x));
            }
        }
    }
}
