namespace TouristicAgency.Models
{
    /*
     * 3. Цели поездок
          Название
     */
    public class Purpose
    {
        // Название
        public string? Name { get; set; }

        // связное свойство для стороны "много"
        public List<Route> Routs { get; set; } = new();
    }
}
