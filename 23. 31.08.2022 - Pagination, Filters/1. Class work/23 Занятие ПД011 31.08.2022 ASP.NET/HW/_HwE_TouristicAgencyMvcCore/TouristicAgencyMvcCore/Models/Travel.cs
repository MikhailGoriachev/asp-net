namespace TouristicAgencyMvcCore.Models
{
    /*
     * Поездки
     *     Клиент
     *     Маршрут 
     *     Дата начала поездки
     *     Длительность поездки   
     */

    public class Travel
    {
        public int Id { get; set; }

        // Клиент - связное свойство 
        public int ClientId { get; set; }
        public Client?  Client { get; set; }

        // Маршрут - связное свойство
        public int RouteId { get; set; }    
        public Route? Route { get; set; }
        
        // Дата начала поездки
        public DateTime Start { get; set; }
        
        // Длительность поездки
        public int Duration { get; set; }

    }
}
