namespace RazorPages_Intro.Models
{
    // сведения о городе
    // название, год основания, герб, население на текущий момент (по открытым данным в Интернет), площадь
    public class City
    {
        // название
        private string _name;
        public string Name {
            get => _name;
            set { _name = value; }
        } // Name

        // год основания
        private int _year;
        public int Year {
            get => _year;
            set {
                if (value < 0 || value > DateTime.Now.Year)
                    throw new InvalidDataException("Недопустимое значение года основания");
                _year = value;
            } // set
        } // Year

        // герб
        private string _arms;
        public string Arms
        {
            get => _arms;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Недопустимое значение в поле имени файла герба");
                _arms = value;
            } // set
        } // Arms

        // население на текущий момент (по открытым данным в Интернет)
        private int _population;
        public int Population {
            get => _population;
            set
            {
                if (value < 0)
                    throw new InvalidDataException("Недопустимое количество населения");
                _population = value;
            } // set
        } // Year

        // площадь (км.кв.)
        private double _area;
        public double Area {
            get => _area;
            set {
                if (value < 0)
                    throw new InvalidDataException("Недопустимое значение площади");
                _area = value;
            }
        } // Area

        // конструкторы
        public City():this("Моспино", 1800, "mospino.png", 10_493, 16.6)
        { }

        public City(string name, int year, string arms, int population, double area) {
            Name = name;
            Year = year;
            Arms = arms;
            Population = population;
            Area = area;
        } // City
    } // class City
}
