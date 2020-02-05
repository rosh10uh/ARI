namespace Chassis.Dapper.Utility
{
    internal class QueryModel
    {
        public string SqlFileName { get; }

        public bool UseRazorTemplate { get; }

        public QueryModel(string sqlFileName, bool useRazorTemplate)
        {
            SqlFileName = sqlFileName;
            UseRazorTemplate = useRazorTemplate;
        }
    }
}
