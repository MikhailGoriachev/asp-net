using HomeWork.Infrastructure;

namespace HomeWork.Models
{
    // Класс Коллекция бытовых приборов
    public class ApplianceList
    {
        // бытовые приборы
        public List<Appliance> Appliances { get; set; }

        #region Конструкторы

        // конструктор по умолчанию
        public ApplianceList() : this(Utils.AppliancesInfo.OrderBy(a => Utils.Rand.Next()).ToList()) { }

        // конструктор инициализирующий
        public ApplianceList(List<Appliance> appliances)
        {
            // установка значений
            Appliances = appliances;
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

        #endregion
    }
}