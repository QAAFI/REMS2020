using System.IO;

using Newtonsoft.Json;

namespace Rems.Infrastructure
{
    public static class JsonTools
    {
        public static T LoadJson<T>(string file)
        {
            using var stream = new StreamReader(file);
            using var reader = new JsonTextReader(stream);

            var serializer = new JsonSerializer()
            {
                Formatting = Formatting.Indented,
                TypeNameHandling = TypeNameHandling.Objects
            };

            return serializer.Deserialize<T>(reader);
        }

        public static void SaveJson(string file, object json)
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

            serializer.Serialize(writer, json);
        }
    }
}
