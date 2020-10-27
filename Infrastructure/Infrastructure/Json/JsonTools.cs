using System.IO;

using Newtonsoft.Json;

namespace Rems.Infrastructure
{
    public static class JsonTools
    {
        public static T LoadJson<T>(string file)
        {
            if (!File.Exists(file)) throw new FileNotFoundException();

            using (var stream = new StreamReader(file))
            using (var reader = new JsonTextReader(stream))
            {
                var serializer = new JsonSerializer()
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Objects
                };

                var t = serializer.Deserialize<T>(reader);
                return t;
            }
        }

        public static void SaveJson(string file, object json)
        {
            using (var stream = new StreamWriter(file))
            using (var writer = new JsonTextWriter(stream))
            {
                writer.CloseOutput = true;
                writer.AutoCompleteOnClose = true;

                var serializer = new JsonSerializer()
                {
                    Formatting = Formatting.Indented,
                    TypeNameHandling = TypeNameHandling.Objects
                };

                serializer.Serialize(writer, json);
            }
        }
    }
}
