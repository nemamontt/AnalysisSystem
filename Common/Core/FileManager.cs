using Common.DTO;
using Common.Enums.ForSolution;
using System.IO;
using System.Text.Encodings.Web;
using System.Text.Json;

namespace Common.Core
{
    public static class FileManager
    {
        public static T? Load<T>(string loadPath, FileType type)
        {
            if (!File.Exists(loadPath))
                return default;
            if (!CheckPath(loadPath))
                return default;

            var value = JsonSerializer.Deserialize<DTO<T>>(File.ReadAllText(loadPath)) ?? null;
            if (value is not null && value.Type == type)
            {
                return value.Value;
            }
            else
            {
                return default;
            }
        }
        public static bool Save<T>(T value, FileType type, string savePath)
        {
            if (!CheckPath(savePath))
                return false;

            var options = new JsonSerializerOptions
            {
                WriteIndented = true,
                Encoder = JavaScriptEncoder.UnsafeRelaxedJsonEscaping
            };
            DTO<T> dto = new()
            {
                Type = type,
                Value = value
            };
            File.WriteAllText(savePath, JsonSerializer.Serialize(dto, options));
            return true;
        }
        public static bool CheckPath(string path)
        {
            foreach (char c in Path.GetInvalidPathChars())
            {
                if (path.Contains(c))
                    return false;
            }
            return true;
        }
    }
}
