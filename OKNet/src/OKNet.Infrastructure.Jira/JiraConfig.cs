using OKNet.Common;

namespace OKNet.Infrastructure.Jira
{
    public class JiraConfig : WindowConfig
    {
        public string ApiHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
    }
}