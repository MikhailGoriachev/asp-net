namespace H_WASP_NET.Models
{
    /// <summary>
    /// Маршруты
    /// *Страна
    /// *Цель поездки
    /// *Стоимость 1 дня пребывания
    /// </summary>
    public class Route
    {
        public int Id { get; set; }

        // Страна назначения
        // связное свойство для стороны "один"
        public int CountryId { get; set; }
        public Country? Country { get; set; }

        // Цель поездки
        // связное свойство для стороны "один"
        public int PurposeTravelId { get; set; }
        public PurposeTravel? PurposeTravel { get; set; }

        // связное свойство для стороны "много"
        public List<Travel> Travels { get; set; } = new();

        // Стоимость 1 дня пребывания
        public int CostDayStay { get; set; }

    } // class Route
}
