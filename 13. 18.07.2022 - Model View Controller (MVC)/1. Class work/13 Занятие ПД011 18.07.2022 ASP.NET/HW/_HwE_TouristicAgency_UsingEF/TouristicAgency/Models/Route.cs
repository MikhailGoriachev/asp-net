namespace TouristicAgency.Models
{
    /*
     * Маршруты
     * Страна
     * Цель поездки
     * Стоимость 1 дня пребывания
     *
     */
    public class Route
    {
        public int Id { get; set; }

        // Страна - связное свойство
        public int CountryId { get; set; }
        public Country? Country { get; set; }

        // Цель поездки - связное свойство
        public int PurposeId { get; set; }
        public Purpose? Purpose { get; set; }

        // Стоимость 1 дня пребывания
        public int DailyCost { get; set; }

        // связное свойство для стороны "много"
        public List<Travel> Travels { get; set; } = new();
    }
}
