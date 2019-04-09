using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatConsolidationApp
{
    /// <summary>
    /// Enum for Chat Services supported
    /// </summary>
    public enum ChatService { Slack };

    /// <summary>
    /// Base class for users on ChatConsolidtionApp
    /// </summary>
    public abstract class BaseUser
    {
        /// <summary>
        /// The string used to uniquely identify user on their service
        /// </summary>
        public string UniqueIdentifierForService { get; set; }
        /// <summary>
        /// Service the user came from
        /// </summary>
        public ChatService ChatService { get; set; }
        /// <summary>
        /// Api Client that get data on User from thier ChatService
        /// </summary>
        protected IUserInfoAPIClient ApiClent { get; set; }
        private Dictionary<string, string> internalToServiceObjectMapping;
        private Type derivedClass;
        /// <summary>
        /// Constructor for BaseUser
        /// </summary>
        /// <param name="service">Chat Service they are associated with</param>
        /// <param name="userName">Unique identifier for their chat service</param>
        /// <param name="apiClient">API Client for getting data on user and thier service</param>
        /// <param name="derivedClass">Dervived class that is being created</param>
        public BaseUser(ChatService service, string userName, IUserInfoAPIClient apiClient, Type derivedClass)
        {
            this.UniqueIdentifierForService = userName;
            this.ChatService = service;
            this.ApiClent = apiClient;
            this.internalToServiceObjectMapping = GetMapping();
            this.derivedClass = derivedClass;
        }

        /// <summary>
        /// Populates Base user with all of the properties from their chat service populated
        /// </summary>
        /// <returns>Populated Base User</returns>
        public async Task<BaseUser> PopulateObjectFromAPI()
        {
            var dataTask = this.GetUserDetails();
            var data = await dataTask;
            PopulateObject(data);
            PopulateComplexProperties(data);
            return this;
        }

        /// <summary>
        /// Populates complex properties from chat service
        /// </summary>
        /// <param name="data">Data to use for population</param>
        protected abstract void PopulateComplexProperties(JObject data);

        /// <summary>
        /// Returns dictionary that maps the internals names of the chat user object to the names on the ChatConsolidationApp object
        /// </summary>
        /// <returns></returns>
        protected abstract Dictionary<string, string> GetMapping();

        /// <summary>
        /// Gets the User details from their chat service api
        /// </summary>
        /// <returns>Object with details about user from their chat service</returns>
        protected abstract Task<JObject> GetUserDetails();

        /// <summary>
        /// Populates simple properties on object based on internalToServiceObjectMapping object
        /// </summary>
        /// <param name="dataToPopulate">Data to populate with</param>
        private void PopulateObject(JObject dataToPopulate)
        {
            if (internalToServiceObjectMapping == null) return;
            foreach(var mapping in internalToServiceObjectMapping)
            {
                var data = dataToPopulate[mapping.Key];
                if (data != null)
                {
                    var propInfo = derivedClass.GetProperty(mapping.Value);
                    var type = propInfo.PropertyType;
                    propInfo.SetValue(this, ConvertToType(data, type));
                }
            }
        }

        /// <summary>
        /// Converts the data to a specifc type if possible
        /// </summary>
        /// <param name="data">Data to convert</param>
        /// <param name="type">Type to convert to</param>
        /// <returns>Data converted to specified type</returns>
        protected dynamic ConvertToType(JToken data, Type type)
        {
            if (data == null) return null;
            if (data.Type == JTokenType.Integer && type == typeof(DateTime))
            {
                return ParseIntsToDateTime(data.ToObject<int>());
            }
            else
            {
                return Convert.ChangeType(data, type);
            }
        }

        /// <summary>
        /// Parses integers to DateTime objects
        /// </summary>
        /// <param name="time">Time represented by an integer</param>
        /// <returns>DateTime object</returns>
        protected abstract DateTime ParseIntsToDateTime(int time);
    }
}
