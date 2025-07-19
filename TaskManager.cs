using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace To_Do_List
{
    public static class TaskManager
    {
        public static List<TaskItem> LoadFile(string path)
        {
            if(!File.Exists(path))
            {
                return new List<TaskItem>();
            }
            var options = new JsonSerializerOptions { Converters = { new JsonStringEnumConverter() } };
            string json = File.ReadAllText(path);
            return JsonSerializer.Deserialize<List<TaskItem>>(json, options) ?? new List<TaskItem>();
        }
        public static void SaveFile(List<TaskItem> tasks, string path)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string json = JsonSerializer.Serialize(tasks, options);
            File.WriteAllText(path, json);
        }
    }
}
