using System;
using System.Collections.Generic;
using System.Linq;

namespace OKNet.Infrastructure.Jira
{
    public static class Jira
    {
        private static readonly Dictionary<JiraStatusCategory, string> StatusCategoryDictionary = new Dictionary<JiraStatusCategory, string>
        {
            { JiraStatusCategory.TO_DO, "new" },
            { JiraStatusCategory.IN_PROGRESS, "indeterminate" },
            { JiraStatusCategory.COMPLETE, "done" },
            { JiraStatusCategory.UNDEFINED, "undefined" }
        };
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
        public static JiraQuery StatusCategoryIs(this JiraQuery query, JiraStatusCategory category)
        {
            return query.Add($"statusCategory={StatusCategoryDictionary[category]}");
        }
        public static JiraQuery StatusCategoryIn(this JiraQuery query, List<JiraStatusCategory> category)
        {
            return query.Add($"statusCategory in ({string.Join(", ",category.Select(statusCategory => StatusCategoryDictionary[statusCategory]))})");
        }
        public static JiraQuery StatusCategoryIsNot(this JiraQuery query, JiraStatusCategory category)
        {
            return query.Add($"statusCategory!={StatusCategoryDictionary[category]}");
        }
        public static JiraQuery OrderBy(this JiraQuery query, string field)
        {
            return query.AddTerm($"order+by+{field}");
        }
    }
}