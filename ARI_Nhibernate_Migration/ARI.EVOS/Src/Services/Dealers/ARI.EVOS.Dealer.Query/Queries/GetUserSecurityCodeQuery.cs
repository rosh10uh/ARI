using Chassis.Query.Annotations;
using Chassis.Query.Interfaces;

namespace ARI.EVOS.Dealer.Query.Queries
{
    /// <summary>
    /// This class is used to set query for get user security code  (Dapper)
    /// </summary>
    [Sql("GetUserSecurityCode")]
    public class GetUserSecurityCodeQuery : IQuery<string>
    {
        public string UserId { get; private set; }
        public GetUserSecurityCodeQuery(string userId)
        {
            UserId = userId;
        }          
     
    }
}
