using System;
using OKNet.Infrastructure.Jira;

namespace OKNet.Infrastructure.Jira
{
    public class APIIssueRequestRoot
    {
        public string expand { get; set; }
        public int startAt { get; set; }
        public int maxResults { get; set; }
        public int total { get; set; }
        public IssueApiModel[] issues { get; set; }
    }

    public class IssueApiModel
    {
        public string expand { get; set; }
        public string id { get; set; }
        public string self { get; set; }
        public string key { get; set; }
        public Fields fields { get; set; }
    }

    public class Fields
    {
        public Issuetype issuetype { get; set; }
        public int? timespent { get; set; }
        public Project project { get; set; }
        public object[] fixVersions { get; set; }
        public int? aggregatetimespent { get; set; }
        public Resolution resolution { get; set; }
        public DateTime? resolutiondate { get; set; }
        public long workratio { get; set; }
        public Watches watches { get; set; }
        public object lastViewed { get; set; }
        public DateTime created { get; set; }
        public Priority priority { get; set; }
        public string[] labels { get; set; }
        public object aggregatetimeoriginalestimate { get; set; }
        public int? timeestimate { get; set; }
        public object[] versions { get; set; }
        public Issuelink[] issuelinks { get; set; }
        public Assignee assignee { get; set; }
        public DateTime updated { get; set; }
        public Status status { get; set; }
        public Component[] components { get; set; }
        public object timeoriginalestimate { get; set; }
        public string description { get; set; }
        public object security { get; set; }
        public int? aggregatetimeestimate { get; set; }
        public string summary { get; set; }
        public Creator creator { get; set; }
        public object[] subtasks { get; set; }
        public Reporter reporter { get; set; }
        public Aggregateprogress aggregateprogress { get; set; }
        public string environment { get; set; }
        public object duedate { get; set; }
        public Progress progress { get; set; }
        public Votes votes { get; set; }
        public Parent parent { get; set; }
    }

    public class Issuetype
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public bool subtask { get; set; }
        public int avatarId { get; set; }
    }

    public class Project
    {
        public string self { get; set; }
        public string id { get; set; }
        public string key { get; set; }
        public string name { get; set; }
        public string projectTypeKey { get; set; }
        public Avatarurls avatarUrls { get; set; }
        public Projectcategory projectCategory { get; set; }
    }


    public class Resolution
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string name { get; set; }
    }

    public class I18nerrormessage
    {
        public string i18nKey { get; set; }
        public object[] parameters { get; set; }
    }

    public class Noneditablereason
    {
        public string reason { get; set; }
        public string message { get; set; }
    }

    public class Watches
    {
        public string self { get; set; }
        public int watchCount { get; set; }
        public bool isWatching { get; set; }
    }

    public class Priority
    {
        public string self { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Assignee
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string accountId { get; set; }
        public string emailAddress { get; set; }
        public Avatarurls1 avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    public class Avatarurls1
    {
        public string _48x48 { get; set; }
        public string _24x24 { get; set; }
        public string _16x16 { get; set; }
        public string _32x32 { get; set; }
    }

    public class Status
    {
        public string self { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public Statuscategory statusCategory { get; set; }
    }

    public class Statuscategory
    {
        public string self { get; set; }
        public int id { get; set; }
        public string key { get; set; }
        public string colorName { get; set; }
        public string name { get; set; }
    }

    public class Creator
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string accountId { get; set; }
        public string emailAddress { get; set; }
        public Avatarurls2 avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    public class Avatarurls2
    {
        public string _48x48 { get; set; }
        public string _24x24 { get; set; }
        public string _16x16 { get; set; }
        public string _32x32 { get; set; }
    }

    public class Reporter
    {
        public string self { get; set; }
        public string name { get; set; }
        public string key { get; set; }
        public string accountId { get; set; }
        public string emailAddress { get; set; }
        public Avatarurls3 avatarUrls { get; set; }
        public string displayName { get; set; }
        public bool active { get; set; }
        public string timeZone { get; set; }
    }

    public class Avatarurls3
    {
        public string _48x48 { get; set; }
        public string _24x24 { get; set; }
        public string _16x16 { get; set; }
        public string _32x32 { get; set; }
    }

    public class Aggregateprogress
    {
        public int progress { get; set; }
        public int total { get; set; }
        public int percent { get; set; }
    }

    public class I18nerrormessage1
    {
        public string i18nKey { get; set; }
        public object[] parameters { get; set; }
    }

    public class I18nerrormessage2
    {
        public string i18nKey { get; set; }
        public object[] parameters { get; set; }
    }

    public class Progress
    {
        public int progress { get; set; }
        public int total { get; set; }
        public int percent { get; set; }
    }

    public class Votes
    {
        public string self { get; set; }
        public int votes { get; set; }
        public bool hasVoted { get; set; }
    }

    public class Parent
    {
        public string id { get; set; }
        public string key { get; set; }
        public string self { get; set; }
        public Fields1 fields { get; set; }
    }

    public class Fields1
    {
        public string summary { get; set; }
        public Status1 status { get; set; }
        public Priority1 priority { get; set; }
        public Issuetype1 issuetype { get; set; }
    }

    public class Status1
    {
        public string self { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public Statuscategory1 statusCategory { get; set; }
    }

    public class Statuscategory1
    {
        public string self { get; set; }
        public int id { get; set; }
        public string key { get; set; }
        public string colorName { get; set; }
        public string name { get; set; }
    }

    public class Priority1
    {
        public string self { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Issuetype1
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public bool subtask { get; set; }
        public int avatarId { get; set; }
    }

    public class Avatarurls4
    {
        public string _48x48 { get; set; }
        public string _24x24 { get; set; }
        public string _16x16 { get; set; }
        public string _32x32 { get; set; }
    }

    public class Issuelink
    {
        public string id { get; set; }
        public string self { get; set; }
        public Type type { get; set; }
        public Inwardissue inwardIssue { get; set; }
    }

    public class Type
    {
        public string id { get; set; }
        public string name { get; set; }
        public string inward { get; set; }
        public string outward { get; set; }
        public string self { get; set; }
    }

    public class Inwardissue
    {
        public string id { get; set; }
        public string key { get; set; }
        public string self { get; set; }
        public Fields2 fields { get; set; }
    }

    public class Fields2
    {
        public string summary { get; set; }
        public Status2 status { get; set; }
        public Priority2 priority { get; set; }
        public Issuetype2 issuetype { get; set; }
    }

    public class Status2
    {
        public string self { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
        public Statuscategory2 statusCategory { get; set; }
    }

    public class Statuscategory2
    {
        public string self { get; set; }
        public int id { get; set; }
        public string key { get; set; }
        public string colorName { get; set; }
        public string name { get; set; }
    }

    public class Priority2
    {
        public string self { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public string id { get; set; }
    }

    public class Issuetype2
    {
        public string self { get; set; }
        public string id { get; set; }
        public string description { get; set; }
        public string iconUrl { get; set; }
        public string name { get; set; }
        public bool subtask { get; set; }
        public int avatarId { get; set; }
    }

    public class Component
    {
        public string self { get; set; }
        public string id { get; set; }
        public string name { get; set; }
    }
}