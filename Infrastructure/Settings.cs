using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

using Rems.Application.Common.Mappings;

namespace Rems.Infrastructure
{
    public sealed class Settings
    {
        [JsonRequired]
        private static HashSet<IPropertyMap> mappings;

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
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\REMS2020";

            Directory.CreateDirectory(folder);

            file = folder + "\\settings.json";

            mappings = new HashSet<IPropertyMap>()
            {
                new PropertyMap("TABLES"),
                new PropertyMap("TRAITS")
            };
        }

        public IPropertyMap this[string name]
        {
            get
            {
                if (mappings.TryGetValue(new PropertyMap(name), out IPropertyMap result))
                    return result;
                else
                    throw new Exception($"No mapping \"{name}\" exists.");
            }
        }

        /// <summary>
        /// Loads the last saved settings
        /// </summary>
        public void Load()
        {
            if (!File.Exists(file)) return;

            instance = JsonTools.LoadJson<Settings>(file);

            loaded = true;
        }

        /// <summary>
        /// Overwrites the saved settings with the current ones
        /// </summary>
        public void Save()
        {
            JsonTools.SaveJson(file, this);
        }

        public void TrackProperty(IPropertyMap mapping)
        {
            mappings.Add(mapping);
        }

        public void UntrackProperty(IPropertyMap mapping)
        {
            mappings.Remove(mapping);
        }

    }
}
