using System;
using System.Collections.Generic;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using OKNet.Core;
using OKNet.Infrastructure.Jira;

namespace OKNet.App
{
    /// <summary>
    /// Jira icons are in svg format, can't be displayed as bitmap/et al
    /// </summary>
    public class JiraIconImageSourceService
    {
        private object @object = new object();

        Dictionary<string, ImageSource> imageCache = new Dictionary<string, ImageSource>();
        private readonly ApiRequestService _apiRequestService;

        public JiraIconImageSourceService(ApiRequestService apiRequestService)
        {
            _apiRequestService = apiRequestService;
        }

        public ImageSource GetImage(JiraConfig jiraConfig, string uri)
        {
            lock (@object)
            {
                if (imageCache.ContainsKey(uri))
                {
                    return imageCache[uri];
                }

                var uriParts = uri.Split('?');
                var lastIndex = uriParts[0].LastIndexOf('/');
                var endpoint = uriParts[0].Substring(lastIndex);
                var result = _apiRequestService.MakeStreamRequestWithBasicAuth(new Uri(uriParts[0]), jiraConfig.Username, jiraConfig.Password, $"?{uriParts[1]}");

                return BitmapFrame.Create(result.Data, BitmapCreateOptions.None, BitmapCacheOption.None);
            }
        }
    }
}