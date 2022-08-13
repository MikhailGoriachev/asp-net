namespace RazorPages_Intro.Models
{
    // данные о парусном корабле:
    // длина в метрах, ширина в метрах, водоизмещение в тоннах, название, год постройки, изображение
    public class Sail
    {
        // тип и название корабля
        private string _header;
        public string Header {
            get => _header;
            set { _header = value; }
        } // Header

        // длина в метрах
        private double _length;
        public double Length {
            get => _length;
            set { _length = value; }
        }

        // ширина в метрах
        private double _width;
        public double Width {
            get => _width;
            set { _width = value; }
        }

        // водоизмещение в тоннах
        private double _displacement;
        public double Displacement {
            get => _displacement;
            set { _displacement = value; }
        }


        // год постройки
        private int _year;
        public int Year {
            get => _year;
            set {
                if (value < 0 || value > DateTime.Now.Year)
                    throw new InvalidDataException("Недопустимое значение года постройки");
                _year = value;
            } // set
        } // Year

        // изображение
        private string _image;
        public string Image {
            get => _image;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Недопустимое значение в поле имени файла изображения");
                _image = value;
            } // set
        } // Image

        // Конструкторы
        public Sail() { }

        public Sail(string header, double length, double width, double displacement, int year, string image) {
            Header = header;
            Length = length;
            Width = width;
            Displacement = displacement;
            Year = year;
            Image = image;
        } // Sail
    } // class Sail
}
