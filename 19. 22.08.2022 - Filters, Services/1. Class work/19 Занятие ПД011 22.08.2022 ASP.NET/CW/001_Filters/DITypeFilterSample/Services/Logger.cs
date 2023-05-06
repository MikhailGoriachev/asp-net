using System.IO;

namespace DITypeFilterSample.Services;

// Реализация сервиса - логгера
public class Logger
{
    public string LogFilePath { get; set; } = "App_Data/log.txt";

    public void WriteLog(string log) {
        var streamWriter = new StreamWriter(LogFilePath, true);

        streamWriter.WriteLine(log);
        streamWriter.WriteLine();

        streamWriter.Close();
    }
}

