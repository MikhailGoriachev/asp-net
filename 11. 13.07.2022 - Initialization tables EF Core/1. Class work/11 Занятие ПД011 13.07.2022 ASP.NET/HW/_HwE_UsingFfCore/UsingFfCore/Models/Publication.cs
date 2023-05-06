namespace UsingFfCore.Models
{
    // Сущность для таблицы ИЗДАНИЯ
    public class Publication
    {
        public int Id { get; set; }

        // Индекс издания по каталогу (строка из цифр)
        public string? PubIndex { get; set; }

        // Вид издания(газета, журнал, альманах, каталог, …)
        public string? Category { get; set; }

        // Наименование издания(название газеты, журнала, …)
        public string? Title { get; set; }

        // Цена 1 экземпляра (в руб.)
        public int Cost { get; set; }

        // Дата начала подписки
        public DateTime Start { get; set; }

        // Длительность подписки (количество месяцев)
        public int Duration { get; set; }
    } // class Publication
}
