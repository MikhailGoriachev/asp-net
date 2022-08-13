namespace RazorPages_Post.Models
{
    // данные о парусном корабле:
    // длина в метрах, ширина в метрах, водоизмещение в тоннах, тип, название, год постройки, изображение
    public class Sail
    {
        // тип корабля
        private string _type;
        public string Type {
            get => _type;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Недопустимое значение в поле типа корабля");

                _type = value;
            }
        } // Type

        // название корабля
        private string _name;
        public string Name {
            get => _name;
            set {
                if (string.IsNullOrWhiteSpace(value))
                    throw new InvalidDataException("Недопустимое значение в поле названия корабля");

                _name = value;
            }
        } // Name


        // длина в метрах
        private double _length;
        public double Length {
            get => _length;
            set {
                if (value <= 0)
                    throw new InvalidDataException("Недопустимое значение в поле длины корабля");

                _length = value;
            }
        } // Length

        // ширина в метрах
        private double _width;
        public double Width {
            get => _width;
            set {
                if (value <= 0)
                    throw new InvalidDataException("Недопустимое значение в поле ширины корабля");

                _width = value;
            }
        } // Width

        // водоизмещение в тоннах
        private double _displacement;
        public double Displacement {
            get => _displacement;
            set {
                if (value <= 0)
                    throw new InvalidDataException("Недопустимое значение в поле водоизмещения корабля");

                _displacement = value;
            }
        } // Displacement


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

        public Sail(string type, string name, double length, double width, double displacement, int year, string image)
        {
            Type = type;
            Name = name;
            Length = length;
            Width = width;
            Displacement = displacement;
            Year = year;
            Image = image;
        } // Sail
    } // class Sail
}
