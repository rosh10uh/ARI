using Chassis.Dapper.Utility;

namespace Chassis.Dapper.Interfaces
{
    internal interface ISqlQuery
    {
        string GetQuery(string resourceNames);
        string GetQuery<TModel>(TModel razorModel, params QueryModel[] queryModels);
    }
}
