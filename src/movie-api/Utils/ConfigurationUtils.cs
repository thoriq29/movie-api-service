using Microsoft.Extensions.Configuration;

namespace Movie.Api.Utils
{
    public class ConfigurationUtils
    {
        public static string MythicAccountUri(IConfiguration configuration) { return configuration.GetConnectionString("AccountService"); }
        public static string UserInfoAccountUri(IConfiguration configuration) { return MythicAccountUri(configuration) + "/admin-account/user-profile"; }
    }
}
