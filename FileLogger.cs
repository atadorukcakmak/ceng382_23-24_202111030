using System.Text.Json;

public static class FileLogger  
{
    public static List<LogRecord>? logRecords;
    private static readonly string? _logFilePath = "LogData.json";

    static FileLogger(){
        deserializeLog();  // Ensure log records are loaded when the class is first accessed
    }
        
    public static void serializeLog()
    {
        if (File.Exists(_logFilePath))
        {
            string jsonString = JsonSerializer.Serialize(logRecords, new JsonSerializerOptions { WriteIndented = true });
            File.WriteAllText(_logFilePath, jsonString);
        }
    }

    public static void deserializeLog(){
        if (File.Exists(_logFilePath))
        {
            string jsonString=File.ReadAllText(_logFilePath);

            if (string.IsNullOrWhiteSpace(jsonString))
            {
                    LogHandler.handleLog("An empty reservations list is initialized");
                    logRecords=new List<LogRecord>();
            }    

            else
            {
                logRecords=JsonSerializer.Deserialize<List<LogRecord>>(jsonString) ?? new List<LogRecord>();
                LogHandler.handleLog("Reservations have been loaded from the JSON file.");  
            }
        }
    
        else
        {
            logRecords=new List<LogRecord>();
        }

    }

}
