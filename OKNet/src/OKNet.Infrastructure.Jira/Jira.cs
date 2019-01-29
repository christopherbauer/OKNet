using System;
using System.Collections.Generic;

namespace OKNet.Infrastructure.Jira
{
    public static class Jira
    {
        private static readonly Dictionary<JiraTimeDifference, string> TimeDictionary = new Dictionary<JiraTimeDifference, string>
        {
            { JiraTimeDifference.Seconds, "s" },
            { JiraTimeDifference.Minutes, "m" },
            { JiraTimeDifference.Hours, "h" },
            { JiraTimeDifference.Days, "d" },
            { JiraTimeDifference.Weeks, "w" }
        };
        public static JiraQuery UpdatedSince(this JiraQuery query, int amount, JiraTimeDifference diff)
        {
            return query.Add($"updated>={amount}{TimeDictionary[diff]}");
        }
        public static JiraQuery UpdatedSince(this JiraQuery query, DateTime time)
        {
            return query.Add($"updated>={time:yyyy-MM-dd}");
        }
        public static JiraQuery ResolvedSince(this JiraQuery query, DateTime time)
        {
            return query.Add($"resolutiondate>={time:yyyy-MM-dd}");
        }
        public static JiraQuery StatusCategoryIs(this JiraQuery query, string category)
        {
            return query.Add($"statusCategory={category}");
        }
        public static JiraQuery OrderBy(this JiraQuery query, string field)
        {
            return query.AddTerm($"order+by+{field}");
        }
    }
}