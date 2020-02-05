using Chassis.Dapper.Utility;

namespace Chassis.Dapper.Interfaces
{
    internal interface IQueryInfo<TQuery>
    {
        QueryModel[] QueryModels
        {
            get;
        }
    }

    internal interface IQueryInfo
    {
        void Init();
    }
}
