namespace H_W1ASP.NET.Models
{
    /// <summary>
    /// Класс город:
    ///  *название,
    ///  *год основания,
    ///  *герб,
    ///  *население на текущий момент,
    ///  *площадь
    /// </summary>
    public class City
    {
        public string NameCity { get; set; }

        public int Year { get; set; }

        public int Population { get; set; }

        public double Square { get; set; }

        public City(string nameCity, double square, int year, int population)
        {
            NameCity = nameCity;
            Year = year;
            Population = population;
            Square = square;
        }

        public override string ToString() => $"Город: {NameCity}, площадью: {Square}м,  {Year} года, популяция: {Population}";


    }// class City
}
