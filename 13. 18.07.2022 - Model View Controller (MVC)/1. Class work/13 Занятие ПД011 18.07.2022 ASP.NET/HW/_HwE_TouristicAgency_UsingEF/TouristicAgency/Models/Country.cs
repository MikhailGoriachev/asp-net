namespace TouristicAgency.Models
{
    /*
     * Страны:
     *     Название
     *     Стоимость транспортных услуг 
     *     Стоимость оформления визы   
     *
     */
    public class Country
    {
        public int Id { get; set; }
        
        // Название
        public string? Name { get; set; }

        // Стоимость транспортных услуг
        public int TransferCost { get; set; }

        // Стоимость оформления визы
        public int VisaCost { get; set; }


        // связное свойство для стороны "много"
        public List<Route> Routs { get; set; } = new();

    } // class Country
}
