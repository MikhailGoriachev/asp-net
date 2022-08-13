using System.IO;
using Home_Work.Utilities;
using Newtonsoft.Json;

namespace Home_Work.Models.Appliances
{
    public class ApplianceStore
    {

        //Коллекция прборов
        private List<Appliance> _appliances;
        public List<Appliance> Appliances
        {
            get
            {
                return _appliances;
            }
            private set
            {
                _appliances = value ?? new List<Appliance>();
            }
        }

        //Путь к JSON-файлу
        private string _filePath;
        public string FilePath
        {
            get => _filePath;
            set
            {
                if (string.IsNullOrWhiteSpace(value))
                    throw new Exception("Строка пути к файлу не может быть пустой!");
                _filePath = value;
            }
        }

        #region Конструкторы
        public ApplianceStore(string FilePath)
        {
            this.FilePath = FilePath;
            //Если файл есть - десериализация, в противном случае генерация
            if (File.Exists(_filePath))
            {
                Desiralize();
            }
            else
            {
                Generate(10);
                Serialize();
            }
        }//ctor

        public ApplianceStore() : this($"{Environment.CurrentDirectory}\\App_Data\\appliances.json")
        {

        }
        #endregion

        //Генерация
        private void Generate(int amount)
        {
            Utils.LastApplianceId = 1;
            _appliances = new List<Appliance>();
            for (int i = 0; i < amount; i++)
            {
                Appliances.Add(new Appliance(Utils.GetApplianceName(), Utils.GetRandom(10_000, 500_000),
                                                           Utils.GetRandom(50, 300) * 100,
                                                           Utils.GetRandom(2, 50)));
            }//for
        }

        //Добавление записи
        public void AddAppliance(Appliance appliance)
        {
            _appliances.Add(appliance ?? new Appliance("---", 0, 0, 0));
            Serialize();
        }

        #region JSON
        //Сериализация 
        public void Serialize()
        {
            string json = JsonConvert.SerializeObject(_appliances);
            File.WriteAllText(_filePath, json);
        }

        //Десериализация
        public void Desiralize()
        {
            string json = File.ReadAllText(_filePath);
            Appliances = JsonConvert.DeserializeObject<List<Appliance>>(json);
        }
        #endregion

        #region Сортировки
        //Сортировка по наименованию
        public IEnumerable<Appliance> OrderByName()
            => _appliances.OrderBy(a => a.Name);

        //Сортировка по убыванию цены
        public IEnumerable<Appliance> OrderByPriceDesc()
            => _appliances.OrderByDescending(a => a.Price);

        //Сортировка по возрастанию количества
        public IEnumerable<Appliance> OrderByQuantityAsc()
            => _appliances.OrderBy(a => a.Quantity);
        #endregion

        //Выборка по диапазону цен
        public IEnumerable<Appliance> SelectByPrice(int Lo, int Hi)
        {
            if (Lo >= Hi)
                return new List<Appliance>();
            return _appliances.Where((a) => a.Price >= Lo && a.Price <= Hi);
        }
    }
}
