using System.Linq;
using System.Reflection;
using Chassis.Dapper.Interfaces;
using Chassis.Query.Annotations;

namespace Chassis.Dapper.Utility
{
    internal sealed class QueryInfo<TQuery> : IQueryInfo<TQuery>, IQueryInfo
    {
        public QueryModel[] QueryModels { get; private set; }

        public void Init()
        {
            var sqlAttributes = typeof(TQuery).GetCustomAttributes<SqlAttribute>();
            if (sqlAttributes != null && sqlAttributes.Count() > 0)
            {
                QueryModels = sqlAttributes.Select(x => new QueryModel(x.SqlFileName, x.UseRazorTemplate)).ToArray();
            }
        }
    }

}
