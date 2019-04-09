using System.Threading.Tasks;

namespace ChatConsolidationApp
{
    class Program
    {
        static void Main(string[] args)
        {
            // Real Slack User Test
            var userName = "UHRLKMYR3";
            var service = ChatService.Slack;
            CreateUserForService(userName, service).GetAwaiter().GetResult();
        }

        /// <summary>
        /// Generates a user based based on the service
        /// </summary>
        /// <param name="userName">Unique identifier of user that they use on the specified service</param>
        /// <param name="service">Chat service the user is coming from</param>
        /// <returns></returns>
        static async Task CreateUserForService(string userName, ChatService service)
        {
            BaseUser user;
            switch (service)
            {
                case ChatService.Slack:
                    user = new SlackUser(userName);
                    user = await user.PopulateObjectFromAPI();
                    break;
                default:
                    break;
            }
        }
    }
}
