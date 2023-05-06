namespace HomeWork.Services;

public class Logger
{
    // название файла
    public string FileName { get; set; } = Path.Combine("App_Data", "application.log");


    // запись в файл
    public async Task WriteLogAsync(string log)
    {
        if (!File.Exists(FileName))
            File.Create(FileName).Close();

        await File.WriteAllTextAsync(FileName, $"[{DateTime.Now:hh:mm:ss dd/MM/yyyy}]: {log}\r\n" + File.ReadAllText(FileName));
    }
}
