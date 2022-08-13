namespace H_WASP_NET.Models
{
    /// <summary>
    /// Цели поездок
    /// *Название
    /// </summary>
    public class PurposeTravel
    {
        public int Id { get; set; }

        // Название
        public string? NamePurpTravel { get; set; }

        // связное свойство для стороны "много"
        public List<Route> Routes { get; set; } = new();

    } // class PurposeTravel
}
