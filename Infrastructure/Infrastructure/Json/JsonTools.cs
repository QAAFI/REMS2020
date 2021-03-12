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

        /// <summary>
        /// Load an object from a JSON file
        /// </summary>
        /// <typeparam name="T">The object type</typeparam>
        /// <param name="file">The source file</param>
        /// <param name="mode">Error handling mode</param>
        public static T LoadJson<T>(string file, JsonLoad mode = JsonLoad.Exception) where T : new()
        {
            // Handle an absent file
            if (!File.Exists(file))
            {
                if (mode == JsonLoad.Exception)
                    throw new FileNotFoundException();
                else if (mode == JsonLoad.New)
                    return new T();
                else
                    return default;
            }

            // Read the file into an object
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

        /// <summary>
        /// Exports an object to a JSON file
        /// </summary>
        /// <param name="file">The file name</param>
        /// <param name="json">The object</param>
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
