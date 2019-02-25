using System.Collections.Generic;
using OKNet.Common;

namespace OKNet.Infrastructure.Jira
{
    public class JiraConfig : WindowConfig
    {
        public string ApiHost { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }
        public int RefreshRate { get; set; }
        public int CleanupRate { get; set; }
        public int PageSize { set; get; }
        public bool PageRotation { get; set; }
        public int PageRotationRate { get; set; }
        public Dictionary<string,string> StatusColors { get; set; }
    }
}