using Chassis.Dapper.Interfaces;
using Chassis.Dapper.Utility;
using Chassis.EmbeddedResource.Interface;
using System.Collections.Generic;
using System.Linq;

namespace Chassis.Dapper.Services
{
    internal class SqlQuery : ISqlQuery
    {
        private readonly IResourceReader _resourceReader;
        private readonly IRazor _razor;

        /// <summary>
        /// Resource Not found key
        /// </summary>
        private string ResourceNotFound { get { return "ResourceNotFound"; } }

        /// <summary>
        /// Initializes object of SqlQuery
        /// </summary>
        /// <param name="resourceReader"></param>
        /// <param name="razor"></param>
        public SqlQuery(IResourceReader resourceReader, IRazor razor)
        {
            _resourceReader = resourceReader;
            _razor = razor;
        }

        public string GetQuery<TModel>(TModel razorModel, params QueryModel[] queryModels)
        {
            var razorQueryModels = queryModels.Where(x => x.UseRazorTemplate);
            string razorQueries = GetRazorQuery(GetCommaSeparatedResourceName(razorQueryModels), razorModel);
            string normalQueries = GetQuery(GetCommaSeparatedResourceName(queryModels.Except(razorQueryModels)));
            return SetQuerySequence(queryModels, razorQueries, normalQueries);
        }

        private string SetQuerySequence(QueryModel[] queryModels, string razorQueries, string normalQueries)
        {
            string[] querySequence = new string[queryModels.Length];

            string[] razor = razorQueries.Split(';').Where(x=> !IsEmpty(x)).ToArray();
            string[] normal = normalQueries.Split(';').Where(x => !IsEmpty(x)).ToArray();
            int razorQueryIndex = 0, normalQueryIndex = 0;

            for (int queryIndex = 0; queryIndex < queryModels.Length; queryIndex++)
            {
                if (queryModels[queryIndex].UseRazorTemplate)
                {
                    querySequence[queryIndex] = razor[razorQueryIndex];
                    razorQueryIndex++;
                }
                else
                {
                    string query = normal[normalQueryIndex];
                    querySequence[queryIndex] = normal[normalQueryIndex];
                    normalQueryIndex++;
                }
            }

            return string.Join("; ", querySequence);
        }

        /// <summary>
        /// Based on the resource name, gets the query and compiles through the razor compiler
        /// </summary>
        /// <typeparam name="TRazor">Razor model type</typeparam>
        /// <param name="resourceName">SQL resource name</param>
        /// <param name="razorModel">Razor model</param>
        /// <returns>Compiled SQL query</returns>
        private string GetRazorQuery<TRazor>(string resourceName, TRazor razorModel)
        {
            if (IsEmpty(resourceName))
                return string.Empty;

            bool isCache = _razor.IsTemplateCached(resourceName, typeof(TRazor));
            string query;
            if (isCache)
            {
                query = _razor.Run(resourceName, razorModel);
            }
            else
            {
                query = GetQuery(resourceName);
                query = _razor.RunCompile(query, resourceName, razorModel);
            }

            return query;
        }

        /// <summary>
        /// Checks if string is empty
        /// </summary>
        /// <param name="value">String value</param>
        /// <returns>Boolean</returns>
        private bool IsEmpty(string value) => string.IsNullOrEmpty(value?.Trim());

        public string GetQuery(string resourceNames)
        {
            if (IsEmpty(resourceNames))
                return string.Empty;

            return string.Join(";", _resourceReader.ReadResource(resourceNames: resourceNames.Split(";")));
        }

        private string GetCommaSeparatedResourceName(IEnumerable<QueryModel> queries)
        {
            return string.Join(";", queries.Select(x => x.SqlFileName));
        }
    }
}
