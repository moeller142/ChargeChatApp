using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ChatConsolidationApp
{
    /// <summary>
    /// Slack Chat User
    /// </summary>
    public class SlackUser : BaseUser
    {
        private const string userField = "user";
        private const string slackProfileField = "profile";
        /// <summary>
        /// Constructor for slack user
        /// </summary>
        /// <param name="userName">Unique Identifier for user on slack</param>
        public SlackUser(string userName) : base(ChatService.Slack, userName, new SlackUserAPIClient(), typeof(SlackUser))
        {
        }
        /// <inheritdoc/>
        protected async override Task<JObject> GetUserDetails()
        {
            var serviceResult = await ApiClent.GetUserInfo(UniqueIdentifierForService);
            return serviceResult[userField] as JObject;
        }
        /// <inheritdoc/>
        protected override DateTime ParseIntsToDateTime(int time)
        {
            var dateTime = new DateTime(1970, 1, 1, 0, 0, 0, DateTimeKind.Utc);
            return dateTime.AddSeconds(time).ToLocalTime();
        }
        /// <inheritdoc/>
        protected override void PopulateComplexProperties(JObject data)
        {
            var slackProfile = data[slackProfileField] as JObject;
            if (slackProfile != null)
            {
                this.Profile = new SlackProfile()
                {
                    AvatarHash = this.ConvertToType(slackProfile["avatar_hash"], typeof(string)),
                    StatusText = this.ConvertToType(slackProfile["status_text"], typeof(string)),
                    StatusEmoji = this.ConvertToType(slackProfile["status_emoji"], typeof(string)),
                    StatusExpiration = this.ConvertToType(slackProfile["status_expiration"], typeof(DateTime)),
                    RealName = this.ConvertToType(slackProfile["real_name"], typeof(string)),
                    DisplayName = this.ConvertToType(slackProfile["display_name"], typeof(string)),
                    RealNameNormalized = this.ConvertToType(slackProfile["real_name_normalized"], typeof(string)),
                    DisplayNameNormalized = this.ConvertToType(slackProfile["display_name_normalized"], typeof(string)),
                    Email = this.ConvertToType(slackProfile["email"], typeof(string)),
                    Image24 = this.ConvertToType(slackProfile["image_24"], typeof(string)),
                    Image32 = this.ConvertToType(slackProfile["image_32"], typeof(string)),
                    Image48 = this.ConvertToType(slackProfile["image_48"], typeof(string)),
                    Image72 = this.ConvertToType(slackProfile["image_72"], typeof(string)),
                    Image192 = this.ConvertToType(slackProfile["image_192"], typeof(string)),
                    Image512 = this.ConvertToType(slackProfile["image_512"], typeof(string)),
                    Team = this.ConvertToType(slackProfile["team"], typeof(string))
                };
            }
        }
        /// <inheritdoc/>
        protected override Dictionary<string, string> GetMapping()
        {
            return new Dictionary<string, string>()
            {
                { "id", nameof(ID) },
                { "team_id", nameof(TeamID) },
                { "name", nameof(Name)},
                { "deleted", nameof(Deleted) },
                { "color", nameof(Color) },
                { "real_name", nameof(RealName) },
                { "tz", nameof(Tz) },
                { "tz_offset", nameof(TzOffset) },
                { "updated", nameof(Updated) },
                { "tz_label", nameof(TzLabel) },
                { "is_admin", nameof(IsAdmin) },
                { "is_owner", nameof(IsOwner) },
                { "is_primary_owner", nameof(IsPrimaryOwner) },
                { "is_ultra_restriced", nameof(IsUltaRestriced) },
                { "is_restricted", nameof(IsRestricted) },
                { "is_bot", nameof(IsBot) },
                { "is_stranger", nameof(IsStranger) },
                { "is_app_user", nameof(IsAppUser) },
                { "has_2fa", nameof(Has2Fa) },
                { "locale", nameof(Locale) }
            };
        }
        // Slack Specific User Properties
        public bool IsRestricted { get; set; }
        public SlackProfile Profile { get; set; }
        public string ID { get; set; }
        public string TeamID { get; set; }
        public string Name { get; set; }
        public bool Deleted { get; set; }
        public string Color { get; set; }
        public string RealName { get; set; }
        public string Tz { get; set; }
        public int TzOffset { get; set; }
        public string TzLabel { get; set; }
        public bool IsAdmin { get; set; }
        public bool IsOwner { get; set; }
        public bool IsPrimaryOwner { get; set; }
        public bool IsUltaRestriced { get; set; }
        public bool IsBot { get; set; }
        public bool IsStranger { get; set; }
        public DateTime Updated { get; set; }
        public bool IsAppUser { get; set; }
        public bool Has2Fa { get; set; }
        public string Locale { get; set; }
    }
    /// <summary>
    /// Slack Profile Object
    /// </summary>
    public class SlackProfile
    {
        public string AvatarHash { get; set; }
        public string StatusText { get; set; }
        public string StatusEmoji { get; set; }
        public DateTime StatusExpiration { get; set; }
        public string RealName { get; set; }
        public string DisplayName { get; set; }
        public string RealNameNormalized { get; set; }
        public string DisplayNameNormalized { get; set; }
        public string Email { get; set; }
        public string ImageOriginal { get; set; }
        public string Image24 { get; set; }
        public string Image32 { get; set; }
        public string Image48 { get; set; }
        public string Image72 { get; set; }
        public string Image192 { get; set; }
        public string Image512 { get; set; }
        public string Team { get; set; }
    }
}
