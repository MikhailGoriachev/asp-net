using HomeWork.Infrastructure;
using HomeWork.Pages;
using Newtonsoft.Json;

namespace HomeWork.Models;

// Класс Коллекция кораблей
public class ShipList
{
    // коллекция кораблей
    public List<Ship> Ships { get; set; }

    // файл для сохранения
    public string FileName { get; set; } = "ships.json";

    // путь к файлу
    private string _path = $"{Environment.CurrentDirectory}/App_Data/";

    #region Конструкторы

    // конструктор по умолчанию
    public ShipList()
    {
        // установка значений
        Ships = new List<Ship>();

        // загрузка данных
        if (!Deserialization())
        {
            Ships = Utils.ShipsInfo.OrderBy(a => Utils.Rand.Next()).ToList();
            Serialization();
        }
    }

    // конструктор инициализирующий
    public ShipList(List<Ship> ships, string fileName = "ships.json")
    {
        // установка значений
        Ships = ships;
        FileName = fileName;

        // сохранение данных
        Serialization();
    }


    #endregion

    #region Методы

    // вывод коллекции, упорядоченной по возрастанию года изготовления
    public List<Ship> OrderByAscYear() => Ships.OrderBy(s => s.Year).ToList();


    // вывод коллекции, упорядоченной по убыванию водоизмещения
    public List<Ship> OrderByDescDisplacement() => Ships.OrderByDescending(s => s.Displacement).ToList();


    // вывод коллекции, упорядоченной по названиям кораблей
    public List<Ship> OrderByName() => Ships.OrderBy(s => s.Name).ToList();


    // сериализация в JSON
    public void Serialization() =>
        File.WriteAllText(_path + FileName, JsonConvert.SerializeObject(Ships));


    // десериализация JSON
    public bool Deserialization()
    {
        // если файл отсутствует
        if (!File.Exists(_path + FileName))
            return false;

        Ships = JsonConvert.DeserializeObject<List<Ship>>(File.ReadAllText(_path + FileName)) ?? new List<Ship>();

        return true;
    }


    #endregion
}
