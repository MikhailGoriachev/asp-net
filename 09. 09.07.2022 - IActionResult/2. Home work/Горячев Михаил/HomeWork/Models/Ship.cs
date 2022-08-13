namespace HomeWork.Models;

// сведения о корабле:
//      - тип
//      - название
//      - длина
//      - ширина
//      - водоизмещение
//      - год постройки
//      - фото корабля

// Класс Корабль
public class Ship
{
    // тип
    public string TypeShip { get; set; }

    // название
    public string Name { get; set; }

    // длина
    public double Length { get; set; }

    // ширина
    public double Width { get; set; }

    // водоизмещение
    public double Displacement { get; set; }

    // год постройки
    public int Year { get; set; }

    // фото корабля
    public string ImageFile { get; set; }

    #region Констркуторы

    // конструктор по умолчанию
    public Ship() : this("", "", 0d, 0d, 0d, 0, "") { }


    // конструктор инициализирующий
    public Ship(string typeShip, string name, double length, double width, double displacement, int year, string imageFile)
    {
        // установка значений
        TypeShip = typeShip;
        Name = name;
        Length = length;
        Width = width;
        Displacement = displacement;
        Year = year;
        ImageFile = imageFile;
    }

    #endregion
}
