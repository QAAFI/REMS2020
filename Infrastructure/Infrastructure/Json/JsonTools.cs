using System.IO;

using Newtonsoft.Json;

namespace Rems.Infrastructure
{
    public static class JsonTools
    {
        public enum JsonLoad
        {
            /// <summary>
            /// Throw an exception if the file is not found
            /// </summary>
            Exception,

            /// <summary>
            /// Return a new T if the file is not found
            /// </summary>
            New
        }

        public static T LoadJson<T>(string file, JsonLoad mode = JsonLoad.Exception) where T : new()
        {
            if (!File.Exists(file))
            {
                if (mode == JsonLoad.Exception)
                    throw new FileNotFoundException();
                else if (mode == JsonLoad.New)
                    return new T();
                else
                    return default;
            }

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
