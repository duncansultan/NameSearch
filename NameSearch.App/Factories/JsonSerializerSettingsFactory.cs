using Newtonsoft.Json;

namespace NameSearch.App.Factories
{
    /// <summary>
    /// JsonSerializerSettings Factory
    /// </summary>
    public static class JsonSerializerSettingsFactory
    {
        /// <summary>
        /// Gets this instance.
        /// </summary>
        /// <returns></returns>
        public static JsonSerializerSettings Get()
        {
            var serializerSettings = new JsonSerializerSettings();
            return serializerSettings;
        }
    }
}