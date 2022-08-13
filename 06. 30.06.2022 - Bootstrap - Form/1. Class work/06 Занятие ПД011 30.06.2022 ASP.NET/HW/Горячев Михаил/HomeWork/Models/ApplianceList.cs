using HomeWork.Infrastructure;
using Newtonsoft.Json;

namespace HomeWork.Models
{
    // Класс Коллекция бытовых приборов
    public class ApplianceList
    {
        // бытовые приборы
        public List<Appliance> Appliances { get; set; } = new List<Appliance>();

        // название файла для сериализации
        public string FileName { get; set; }

        // путь к файлу
        private string _path = $"{Environment.CurrentDirectory}/App_Data/";

        #region Конструкторы

        // конструктор по умолчанию
        public ApplianceList()
        {
            // установка значений
            FileName = "appliances.json";

            // загрузка данных
            if (!Deserialization())
            {
                Appliances = Utils.AppliancesInfo.OrderBy(a => Utils.Rand.Next()).ToList();
                Serialization();
            }
        }

        // конструктор инициализирующий
        public ApplianceList(List<Appliance> appliances, string fileName = "appliances.json")
        {
            // установка значений
            Appliances = appliances;
            FileName = fileName;

            // сохранение данных
            Serialization();
        }

        #endregion

        #region Методы

        // упорядочивание коллекции по наименованию
        public List<Appliance> OrderByName() => Appliances.OrderBy(a => a.Name).ToList();


        // упорядочивание коллекции по убыванию цены
        public List<Appliance> OrderByDescPrice() => Appliances.OrderByDescending(a => a.Price).ToList();


        // упорядочивание коллекции по возрастанию количества
        public List<Appliance> OrderByAmount() => Appliances.OrderBy(a => a.Amount).ToList();


        // выборка коллекции – товары с ценой, попадающей в заданный диапазон
        public List<Appliance> SelectByPriceRange(int min, int max) =>
            Appliances.Where(a => a.Price >= min && a.Price <= max).ToList();


        // сериализация в JSON
        public void Serialization() =>
            File.WriteAllText(_path + FileName, JsonConvert.SerializeObject(Appliances));


        // десериализация JSON
        public bool Deserialization()
        {
            // если файл отсутствует
            if (!File.Exists(_path + FileName))
                return false;

            Appliances = JsonConvert.DeserializeObject<List<Appliance>>(File.ReadAllText(_path + FileName)) ?? new List<Appliance>();

            return true;
        }

        #endregion
    }
}