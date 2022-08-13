namespace Homework.Models.Ships;

public class Ship
{
    // тип корабля
    private string _type = null!;
    public string Type
    {
        get => _type;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidDataException("Недопустимое значение в поле типа корабля");

            _type = value;
        }
    }

    // название корабля
    private string _name = null!;
    public string Name
    {
        get => _name;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidDataException("Недопустимое значение в поле названия корабля");

            _name = value;
        }
    }


    // длина в метрах
    private double _length;
    public double Length
    {
        get => _length;
        set
        {
            if (value <= 0)
                throw new InvalidDataException("Недопустимое значение в поле длины корабля");

            _length = value;
        }
    }

    // ширина в метрах
    private double _width;
    public double Width
    {
        get => _width;
        set
        {
            if (value <= 0)
                throw new InvalidDataException("Недопустимое значение в поле ширины корабля");

            _width = value;
        }
    }

    // водоизмещение в тоннах
    private double _displacement;
    public double Displacement
    {
        get => _displacement;
        set
        {
            if (value <= 0)
                throw new InvalidDataException("Недопустимое значение в поле водоизмещения корабля");

            _displacement = value;
        }
    }


    // год постройки
    private int _year;
    public int Year
    {
        get => _year;
        set
        {
            if (value < 0 || value > DateTime.Now.Year)
                throw new InvalidDataException("Недопустимое значение года постройки");

            _year = value;
        }
    }

    // изображение
    private string _image = null!;
    public string Image
    {
        get => _image;
        set
        {
            if (string.IsNullOrWhiteSpace(value))
                throw new InvalidDataException("Недопустимое значение в поле имени файла изображения");

            _image = value;
        }
    }

    // Конструкторы
    public Ship()
    {
    }

    public Ship(string name, string type, double length, double width, double displacement, int year, string 
    image)
    {
        Type = type;
        Name = name;
        Length = length;
        Width = width;
        Displacement = displacement;
        Year = year;
        Image = image;
    }
    public Ship(string name, string type, double length, double width, double displacement, int year)
    {
        Type = type;
        Name = name;
        Length = length;
        Width = width;
        Displacement = displacement;
        Year = year;
    }
    
    public Ship(Ship ship)
    {
        Type = ship.Type;
        Name = ship.Name;
        Length = ship.Length;
        Width = ship.Width;
        Displacement = ship.Displacement;
        Year = ship.Year;
    }
}