namespace HomeWork.Models;

// Класс Периодическое издание
//      - индекс издания по каталогу (строка из цифр)
//      - вид издания (газета, журнал, альманах, каталог, …)
//      - наименование издания (название газеты, журнала, …)
//      - цена 1 экземпляра (в руб.)
//      - дата начала подписки
//      - длительность подписки (количество месяцев)

public class Periodical
{
    // идентификатор
    public int Id { get; set; }

    // индекс издания по каталогу (строка из цифр)
    public string? Index { get; set; }

    // вид издания
    public string? TypeEdition { get; set; }

    // наименование издания
    public string? Name { get; set; }

    // цена 1 экземпляра (в руб.)
    public int Price { get; set; }

    // дата начала подписки
    public DateTime Date { get; set; }

    // длительность подписки (количество месяцев)
    public int Duration { get; set; }
}
