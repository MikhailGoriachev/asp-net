namespace H_WASP_NET.Models
{
    /// <summary>
    /// Страны	
    /// *Название
    /// *Стоимость транспортных услуг
    /// *Стоимость оформления визы
    /// </summary>
    public class Country
    {
        public int Id { get; set; }

        // Название
        public string? NameCountry { get; set; }

        // Стоимость транспортных услуг
        public int CostTransportServ { get; set; }

        // Стоимость оформления визы
        public int CostVisa { get; set; }


        // связное свойство для стороны "много"
        public List<Route> Routes { get; set; } = new();

    } // class Country
}
