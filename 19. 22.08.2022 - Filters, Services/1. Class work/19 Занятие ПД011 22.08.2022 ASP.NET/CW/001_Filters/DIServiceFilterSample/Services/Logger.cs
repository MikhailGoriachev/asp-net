using System.IO;

namespace DITypeFilterSample.Services;

public class Logger
{
    public string LogFilePath { get; set; } = "App_Data/log.txt";

    public void WriteLog(string log) {
        using var streamWriter = new StreamWriter(LogFilePath, true);

        streamWriter.WriteLine(log);
        streamWriter.WriteLine();
    } // WriteLog
}

