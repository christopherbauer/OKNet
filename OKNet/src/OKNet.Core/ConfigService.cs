using System.IO;
using Newtonsoft.Json;

namespace OKNet.Core
{
    public class ConfigService : IConfigService
    {
        public T GetConfig<T>(string path) where T : new()
        {
            var settingsString = File.ReadAllText(path);
            return JsonConvert.DeserializeObject<T>(settingsString, new WindowConfigConverter());
        }
    }
}
