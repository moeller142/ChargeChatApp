using Newtonsoft.Json.Linq;
using System;
using System.Threading.Tasks;

namespace ChatConsolidationApp
{
    /// <summary>
    /// Base Class for API Invocation
    /// </summary>
    public abstract class BaseAPIInvoker : IUserInfoAPIClient
    {
        /// <summary>
        /// base path for the API
        /// </summary>
        protected string basePath;

        /// <summary>
        /// Gets the user attibutes based on the unique identifier
        /// </summary>
        /// <param name="userName">Unique identifier for user</param>
        /// <returns>JObject with users attributes</returns>
        public abstract Task<JObject> GetUserInfo(string userName);

        protected UriBuilder addToQuery(UriBuilder baseUri, string[] queriesToAppend)
        {
            if (queriesToAppend.Length == 0) return baseUri;
            var queryString = baseUri.Query;
            foreach (var query in queriesToAppend)
            {
                if (queryString.Length != 0)
                {
                    queryString += "&";
                }
                queryString += query;

            }
            baseUri.Query = queryString;
            return baseUri;
        }

        protected string getQueryString(string key, string value)
        {
            return key + "=" + value;
        }

    }
}
