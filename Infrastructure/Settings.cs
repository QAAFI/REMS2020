using System;
using System.Collections.Generic;
using System.Linq;
using System.IO;

using Newtonsoft.Json;

using Rems.Application.Common.Mappings;

namespace Rems.Infrastructure
{
    public sealed class Settings
    {
        [JsonRequired]
        private HashSet<IPropertyMap> mappings;

        [JsonIgnore]
        private readonly string file;

        [JsonIgnore]
        public bool Loaded { get; set; } = false;

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
                var map = mappings.FirstOrDefault(m => m.Name == name);

                if (map != null)
                    return map;
                else
                    throw new Exception($"No mapping \"{name}\" exists.");
            }
        }

        public bool IsMapped(string name)
        {
            if (mappings.Any(m => m.Name == name)) 
                return true;
            else
                return false;
        }

        /// <summary>
        /// Loads the last saved settings
        /// </summary>
        public void Load()
        {
            if (!File.Exists(file)) return;

            mappings = JsonTools.LoadJson<HashSet<IPropertyMap>>(file);

            Loaded = true;
        }

        /// <summary>
        /// Overwrites the saved settings with the current ones
        /// </summary>
        public void Save()
        {
            JsonTools.SaveJson(file, mappings);
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
