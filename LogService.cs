using System;
using System.Collections.Generic;
using System.Linq;

public static class LogService
{
    public static void DisplayLogsByName(string name)
    {
        
        Console.WriteLine("Showing logs by name: ");
        

        FileLogger.deserializeLog(); 

        if (FileLogger.logRecords != null)
        {
            foreach (LogRecord logRecord in FileLogger.logRecords)
            {
                if (logRecord.Message.Contains(name, StringComparison.OrdinalIgnoreCase))
                {
                    Console.WriteLine($"{logRecord.Timestamp}: {logRecord.Message}");
                }
            }
        }
      

    }


    public static void DisplayLogs(DateTime start, DateTime end)
    {
        Console.WriteLine("Showing the logs between start and end time: ");

        FileLogger.deserializeLog(); 

        if (FileLogger.logRecords != null)
        {
            foreach (LogRecord logRecord in FileLogger.logRecords)
            {
                if (logRecord.Timestamp<=end && logRecord.Timestamp >= start)
                {
                    Console.WriteLine($"{logRecord.Timestamp}: {logRecord.Message}");
                }
            }
        }
    }

}