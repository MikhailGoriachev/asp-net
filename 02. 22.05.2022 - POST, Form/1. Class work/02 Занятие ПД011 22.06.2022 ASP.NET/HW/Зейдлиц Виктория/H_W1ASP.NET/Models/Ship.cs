namespace H_W1ASP.NET.Models
{
    /// <summary>
    /// Класс корабль:
    /// *длина в метрах,
    /// *ширина в метрах,
    /// *водоизмещение в тоннах,
    /// *название,
    /// *год постройки,
    /// *изображение
    /// </summary>
    public class Ship
    {
        public double Lengh { get; set; }

        public double Width { get; set; }

        public int Displacement { get; set; }
        public string Name { get; set; }

        public int Year { get; set; }

        public Ship(string name, double lenght, double width, int displacement,  int year)
        {
            Lengh = lenght;
            Width = width;
            Displacement = displacement;
            Name = name;
            Year = year;
        }
        public override string ToString() => $"Корабль {Name}: '{Name}'; длина {Lengh}м; ширина {Width}м; водоизмещение {Displacement}т ; {Year}г. постройки)";

    }// class Ship
}
