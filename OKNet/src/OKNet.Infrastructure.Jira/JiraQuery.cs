using System.Collections.Generic;
using System.Text;

namespace OKNet.Infrastructure.Jira
{
    public class JiraQuery
    {
        private string _queryBase = "?jql=";
        private string _queryText;
        private string _additionalTerms;
        
        internal virtual JiraQuery Add(string s)
        {
            if (!string.IsNullOrEmpty(_queryText))
                _queryText += "+AND+" + s.Replace(" ", "%20");
            else
            {
                _queryText = s.Replace(" ", "%20");
            }

            return this;
        }

        public JiraQuery AddTerm(string s)
        {
            _additionalTerms += "&" + s.Replace(" ", "%20");
            return this;
        }

        public override string ToString() => $"{_queryBase}{_queryText}{_additionalTerms}";
    }
}
