namespace Homework.Models;

public class Publication
{
    // Id
    public int PublicationId  { get; set; }

    // Индекс издания по каталогу (строка из цифр)
    public string? PubIndex    { get; set; }

    // Название издания
    public string? Title       { get; set; }

    // Цена 1 экземпляра (в руб.)
    public int Price          { get; set; }
    
    // Дата начала подписки
    public DateTime StartDate { get; set; }
    
    // Длительность подписки (количество месяцев)
    public int Duration       { get; set; }
    
    
    // Вид издания (газета, журнал, альманах, каталог, …)
    // связное свойство для стороны "один"
    public int CategoryId { get; set; }
    public virtual Category? Category { get; set; }
}