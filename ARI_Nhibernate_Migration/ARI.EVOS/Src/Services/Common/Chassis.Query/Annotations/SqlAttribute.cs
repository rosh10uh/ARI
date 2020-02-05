using System;

namespace Chassis.Query.Annotations
{
    [AttributeUsage(AttributeTargets.Class, AllowMultiple = true)]
    public class SqlAttribute : Attribute
    {
        public string SqlFileName { get; }

        public bool UseRazorTemplate { get; }

        public SqlAttribute(string sqlFileName, bool useRazorTemplate = false)
        {
            SqlFileName = sqlFileName;
            UseRazorTemplate = useRazorTemplate;
        }
    }
}
