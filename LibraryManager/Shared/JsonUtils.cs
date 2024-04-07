using LibraryManager.Model;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text.Json;

namespace LibraryManager.Shared;

public class JsonUtils
{
    private static readonly string BaseDirectory = AppDomain.CurrentDomain.BaseDirectory;
    private static readonly string LoggerPath = Path.Combine(BaseDirectory, "logs.json");

    public static void SaveLogs(List<Log> logs)
    {
        var options = new JsonSerializerOptions
        {
            WriteIndented = true
        };

        var jsonString = JsonSerializer.Serialize(logs, options);
        File.WriteAllText(LoggerPath, jsonString);
    }
}