public static class LogHandler
{
    
    public static void handleLog(string message){


        LogRecord logRecord=new LogRecord(message, DateTime.Now);
        FileLogger.logRecords?.Add(logRecord);
        FileLogger.serializeLog();
        Console.WriteLine($"[{DateTime.Now}] {message}");
    }





}
