using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;
using Newtonsoft.Json.Serialization;

namespace WindowsClient
{
    public sealed class Settings
    {
        [JsonRequired]
        private static Dictionary<string, DistinctDictionary<string, string>> properties;

        [JsonIgnore]
        private readonly string file;

        [JsonIgnore]
        private static bool loaded = false;

        [JsonIgnore]
        public bool Loaded => loaded;

        [JsonIgnore]
        private static Settings instance = new Settings();

        [JsonIgnore]
        public static Settings Instance => instance;

        private Settings()
        {
            properties = new Dictionary<string, DistinctDictionary<string, string>>();
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\REMS2020";

            Directory.CreateDirectory(folder);

            file = folder + "\\settings.json";
        }

        public DistinctDictionary<string, string> this[string name]
        {
            get
            {
                return properties[name];
            }
        }

        public void Load()
        {
            if (!File.Exists(file)) return;

            using var stream = new StreamReader(file);
            using var reader = new JsonTextReader(stream);

            var serializer = new JsonSerializer()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects
            };

            var node = serializer.Deserialize<Settings>(reader);

            loaded = true;
        }

        public void Save()
        {
            using var stream = new StreamWriter(file);

            using var writer = new JsonTextWriter(stream)
            {
                CloseOutput = true,
                AutoCompleteOnClose = true
            };

            var serializer = new JsonSerializer()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects
            };

            serializer.Serialize(writer, this);
        }

        public void TrackProperty(string property)
        {
            if (properties.ContainsKey(property))
            {
                throw new Exception("A property by that name is already being tracked.");
            }

            properties.Add(property, new DistinctDictionary<string, string>());
        }

        public void UntrackProperty(string property)
        {
            properties.Remove(property);
        }
    }
}
