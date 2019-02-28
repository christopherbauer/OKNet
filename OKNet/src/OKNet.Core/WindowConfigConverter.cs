using System;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using OKNet.Common;
using OKNet.Infrastructure.Jira;
using Type = System.Type;

namespace OKNet.Core
{
    /// <summary>
    /// Custom configuration for deserializing the window config
    /// </summary>
    public class WindowConfigConverter : JsonConverter
    {
        //Props https://stackoverflow.com/a/46175763/82333
        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            //We don't currently write configs
            throw new NotImplementedException();
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            JObject jo = JObject.Load(reader);
            var typeCode = jo["type"].Value<string>();
            switch (typeCode)
            {
                case "jira-inprogress":
                case "jira-complete":
                    return jo.ToObject<JiraConfig>();
                default:
                    throw new NotImplementedException($"Window type {typeCode} not implemented in {nameof(WindowConfigConverter)}");
            }
        }

        public override bool CanWrite => false;

        public override bool CanConvert(Type objectType)
        {
            return objectType == typeof(WindowConfig);
        }
    }
}