namespace BootstrapFileUpload.Models.Task02
{
    // сведения о корабле:
    // тип, название, длина, ширина, водоизмещение, год постройки, фото корабля
    public class Ship
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
        public Ship() { }

        public Ship(string type, string name, double length, double width, double displacement, int year, string image)
        {
            Type = type;
            Name = name;
            Length = length;
            Width = width;
            Displacement = displacement;
            Year = year;
            Image = image;
        } // Ship
    } // class Ship
}
