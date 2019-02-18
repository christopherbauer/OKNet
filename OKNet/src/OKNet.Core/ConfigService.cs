using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Reflection;
using Microsoft.Configuration.ConfigurationBuilders;

namespace OKNet.Core
{
    public class ConfigService : IConfigService
    {
        private SimpleJsonConfigBuilder _simpleJsonConfigBuilder;

        public ConfigService()
        {
            Initialize();
        }
        public void Initialize()
        {
            var section = ConfigurationManager.GetSection("configBuilders");
            var builderConfig = (ConfigurationBuildersSection)section;

            _simpleJsonConfigBuilder = new SimpleJsonConfigBuilder();
            _simpleJsonConfigBuilder.Initialize("SimpleJson", builderConfig.Builders["SimpleJson"].Parameters);
        }

        public T GetConfig<T>(string path) where T : new()
        {
            var props = typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance | BindingFlags.SetProperty);

            var tReturn = new T();
            var collection = _simpleJsonConfigBuilder.GetAllValues(path);
            foreach (var propertyInfo in props)
            {
                if (collection.Any(pair => string.Equals(pair.Key.Substring(path.Length + 1), propertyInfo.Name,
                    StringComparison.CurrentCultureIgnoreCase)))
                {
                    var keyValuePair = collection.Single(pair => string.Equals(pair.Key.Substring(path.Length + 1),
                        propertyInfo.Name, StringComparison.CurrentCultureIgnoreCase));
                    propertyInfo.SetValue(tReturn, keyValuePair.Value);
                }
            }

            return tReturn;
        }

        public IEnumerable<string> GetNames(string path)
        {
            return Enumerable.Select(_simpleJsonConfigBuilder.GetAllValues(string.Empty).Where(pair => pair.Key.Substring(0, path.Length) == path), pair => pair.Key.Substring(path.Length).Split(new[] { ":" }, StringSplitOptions.RemoveEmptyEntries)[0]).Distinct();
        }

        public T Get<T>(string keyPath)
        {
            return (T)Convert.ChangeType(_simpleJsonConfigBuilder.GetAllValues(keyPath).Single().Value, typeof(T));
        }
    }
}
