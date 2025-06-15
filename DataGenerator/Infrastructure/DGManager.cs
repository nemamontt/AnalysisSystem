using Common.DTO;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace DataGenerator.Infrastructure
{
    public class DGManager
    {
        public static void SaveDatabase<T>(string puth, DTO<T> database)
        {
            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            File.WriteAllText(puth, JsonSerializer.Serialize(database, options));
        }
        public static object OpenDatabase(string puth)
        {
            return JsonSerializer.Deserialize<object>(File.ReadAllText(puth));
        }
    }
}
