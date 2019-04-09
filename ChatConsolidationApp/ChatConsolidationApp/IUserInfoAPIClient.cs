using Newtonsoft.Json.Linq;
using System.Threading.Tasks;

namespace ChatConsolidationApp
{
    /// <summary>
    /// Interface for API Invocation to get user data
    /// </summary>
    public interface IUserInfoAPIClient
    {
        /// <summary>
        /// Gets info for user by user name
        /// </summary>
        /// <param name="userName">unique identifier for user</param>
        /// <returns>JOject with data about user</returns>
        Task<JObject> GetUserInfo(string userName);
        
    }
}
