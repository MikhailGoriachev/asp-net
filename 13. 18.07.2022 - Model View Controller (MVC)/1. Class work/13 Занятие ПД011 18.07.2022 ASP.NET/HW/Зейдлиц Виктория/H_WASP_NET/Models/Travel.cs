namespace H_WASP_NET.Models
{
    /// <summary>
    /// Поездки
    /// *Клиент
    /// *Маршрут
    /// *Дата начала поездки
    /// *Длительность поездки
    /// </summary>
    public class Travel
    {
        public int Id { get; set; }

        // Клиент
        // связное свойство для стороны "один"
        public int ClientId { get; set; }
        public Client? Client { get; set; }

        // Маршрут
        // связное свойство для стороны "один"
        public int RouteId { get; set; }    
        public Route? Route { get; set; }

        // Дата начала поездки
        public DateTime StartTravel { get; set; }

        // Длительность поездки
        public int Duration { get; set; }


    } // class Travel
}
