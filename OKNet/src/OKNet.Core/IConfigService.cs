using System.Collections.Generic;

namespace OKNet.Core
{
    public interface IConfigService
    {
        T GetConfig<T>(string path) where T : new();
        IEnumerable<string> GetNames(string path);
        T Get<T>(string keyPath);
    }
}
