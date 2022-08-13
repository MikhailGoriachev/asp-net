namespace TouristicAgency.Models
{
    /*
     * Цели поездок
     *     Название цели поездки
     */
    public class Purpose
    {
        public int Id { get; set; }

        // Название
        public string? Name { get; set; }

        // связное свойство для стороны "много"
        public List<Route> Routs { get; set; } = new();
    }
}
