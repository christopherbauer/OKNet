namespace OKNet.Core
{
    public interface IConfigService
    {
        T GetConfig<T>(string path) where T : new();
    }
}
