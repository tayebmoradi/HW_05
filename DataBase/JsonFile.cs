using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Threading.Tasks;

namespace DataBase
{
    public static class JsonFile
    {
        private static readonly JsonSerializerOptions _SerializerOptions =
            new() { DefaultIgnoreCondition = System.Text.Json.Serialization.JsonIgnoreCondition.WhenWritingNull };
        public static void SimpleWrite(object obj, string fileName)
        {
            var JsonString = JsonSerializer.Serialize(obj, _SerializerOptions);
            File.WriteAllText(fileName, JsonString);
        }
        public static string SimpleRead(string fileName)
        {
            using StreamReader streamReader = new(fileName);
            var json = streamReader.ReadToEnd();
            return json;
        }
    }
}
