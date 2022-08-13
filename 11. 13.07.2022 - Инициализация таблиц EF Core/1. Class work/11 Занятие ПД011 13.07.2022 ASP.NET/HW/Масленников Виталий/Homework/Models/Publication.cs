namespace Homework.Models;

public class Publication
{
    // Id
    public int PublicationId  { get; set; }

    // Индекс издания по каталогу (строка из цифр)
    public string? PubIndex    { get; set; }

    // Тип издания
    public string? PubType     { get; set; }
        
    // Название издания
    public string? Title       { get; set; }

    // Цена 1 экземпляра (в руб.)
    public int Price          { get; set; }
    
    // Дата начала подписки
    public DateTime StartDate { get; set; }
    
    // Длительность подписки (количество месяцев)
    public int Duration       { get; set; }
}