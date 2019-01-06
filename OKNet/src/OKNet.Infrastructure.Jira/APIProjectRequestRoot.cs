using System;
using System.Collections.Generic;
using System.Text;

namespace OKNet.Infrastructure.Jira
{
    public class APIProjectRequestRoot
    {
        public ProjectApiModel[] Property1 { get; set; }
    }
    public class ProjectApiModel
    {
        public string expand { get; set; }
        public string self { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public Avatarurls avatarUrls { get; set; }
        public string projectTypeKey { get; set; }
        public bool simplified { get; set; }
        public string style { get; set; }
        public bool isPrivate { get; set; }
        public Projectcategory projectCategory { get; set; }
    }
 }
