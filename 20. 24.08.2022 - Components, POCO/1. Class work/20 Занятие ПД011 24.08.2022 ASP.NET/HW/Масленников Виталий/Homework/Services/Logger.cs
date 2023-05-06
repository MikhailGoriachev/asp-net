namespace Homework.Services;

public class Logger
{
    private string LogFilePath { get; set; } = "App_Data/application.txt";

    public void WriteLog(string log) =>
        File.AppendAllText(LogFilePath, log + Environment.NewLine, System.Text.Encoding.UTF8);
}