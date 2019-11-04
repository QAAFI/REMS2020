using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Xml.Serialization;

namespace WindowsClient
{
    public sealed class Settings
    {
        private static Dictionary<string, DistinctDictionary<string, string>> properties
            = new Dictionary<string, DistinctDictionary<string, string>>();

        [NonSerialized]
        private readonly string file;

        [NonSerialized]
        private static bool loaded = false;

        public bool Loaded => loaded;

        [NonSerialized]
        private static Settings instance = new Settings();

        public static Settings Instance => instance;

        private Settings()
        {            
            string folder = Environment.GetFolderPath(Environment.SpecialFolder.LocalApplicationData) + "\\REMS2020";

            Directory.CreateDirectory(folder);

            file = folder + "\\settings.xml";
        }

        public Dictionary<string, string> this[string name]
        {
            get
            {
                return properties[name];
            }
        }

        public void Load()
        {
            if (!File.Exists(file)) return;

            var serializer = new XmlSerializer(GetType());
            using var reader = new StreamReader(file);
            instance = serializer.Deserialize(reader) as Settings;

            loaded = true;
        }

        public void Save()
        {
            var serializer = new XmlSerializer(GetType());
            using var writer = new StreamWriter(file);
            serializer.Serialize(writer, instance);
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

        //public void TrackClasses<T>(ICollection<T> items) where T : class
        //{
        //    foreach (var item in items)
        //    {
        //        string key = item.GetType().Name;

        //        var values = new DistinctDictionary<string, string>();

        //        foreach(var property in item.GetType().GetProperties())
        //        {
        //            values.Add(property.Name, property.Name.ToLower());
        //        }

        //        properties.Add(key, values);
        //    }
        //}
    }
}
