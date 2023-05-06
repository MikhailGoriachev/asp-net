using System.IO;

namespace Home_work.Services;

public class Logger
{
    //Путь к log-файлу
    public string LogFile { get; set; } = "App_Data/application.log";

    //Синхронный метод записи 
    public void WrietInLog(string log)
    {
        File.AppendAllText(LogFile,log + "\n");
    }

    public async void WrietInLogAync(string log)
    {
        await File.AppendAllTextAsync(LogFile,log + "\n");
    }
}
