using Newtonsoft.Json.Linq;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace ChatConsolidationApp
{
    /// <summary>
    /// API Client to get data about slack users
    /// </summary>
    public class SlackUserAPIClient : BaseAPIInvoker
    {
        private const string token = "token";
        private const string user = "user";

        /// <summary>
        /// Constructor for SlackUserAPIClient
        /// </summary>
        public SlackUserAPIClient()
        {
            basePath = "https://slack.com/api/";
        }
        /// <inheritdoc/>
        public override async Task<JObject> GetUserInfo(string userName)
        {
            using (HttpClient client = new HttpClient())
            {
                var builder = new UriBuilder(basePath + "users.info");
                var queriesToAppnd = new string[] { getQueryString(token, GetToken()), getQueryString(user, userName) };
                builder = addToQuery(builder, queriesToAppnd);
                var response = await client.GetAsync(builder.Uri);
                if(response.IsSuccessStatusCode)
                {
                    var toReturn = await response.Content.ReadAsStringAsync();
                    return JObject.Parse(toReturn);
                }
                
                return null;
            }
        }

        private string GetToken()
        {
            return "xoxp-604326463415-603699746853-603708387524-15106b67dc8ec0544e62f4d89d0626eb";
        }
    }
}
