using H_W6ASP_NET.Models.Task1;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Newtonsoft.Json;

namespace H_W6ASP_NET.Pages
{
    public class AppliancesModel : PageModel
    {

        // коллекция данных о бытовой технике
        private static List<Appliance> _appliances = new List<Appliance>();

        public List<Appliance> Appliances = new List<Appliance>();

        // полное имя файла для записи/чтения
        private string _path = $"{Environment.CurrentDirectory}/App_Data/appliance.json";


        // обработчик GET-запроса к странице
        public void OnGet()
        {
            if (System.IO.File.Exists(_path))
            {
                Deserialize();
            }
            else {
                // формирование коллекции
                _appliances = new List<Appliance>(new[]
                {
                    new Appliance("Стиральная машина",    "Крупная быт.техника", "OC8532", 34000, 3, "appliance01.jpg"),
                    new Appliance("Духовой шкаф",         "Крупная быт.техника", "OC8642", 31000, 2, "appliance02.jpg"),
                    new Appliance("Электрический чайник", "Мелкая быт.техника", "BK2591", 1500, 5, "appliance03.jpg"),
                    new Appliance("Фен",                  "Мелкая быт.техника", "BK8368", 5050, 7, "appliance04.jpg"),
                    new Appliance("Пылесос",              "Мелкая быт.техника", "BK8420", 6300, 3, "appliance05.jpg"),
                    new Appliance("Кухонный комбайн",     "Крупная быт.техника", "OC8523", 8200, 2, "appliance06.jpg"),
                    new Appliance("Микроволновая печь",   "Мелкая быт.техника", "BK8307", 4000, 4, "appliance07.jpg"),
                    new Appliance("Мультиварка",          "Мелкая быт.техника", "BK8592", 5400, 2, "appliance08.jpg"),
                    new Appliance("Холодильник",          "Крупная быт.техника", "OC9132", 44000, 3, "appliance09.jpg"),
                    new Appliance("Посудомоечная машина", "Крупная быт.техника", "OC8824", 36000, 2, "appliance10.jpg"),
                    new Appliance("Телевизор",            "Крупная быт.техника", "OC4612", 28000, 4, "appliance11.jpg"),
                    new Appliance("Кондиционер",          "Крупная быт.техника", "OC9524", 26400, 5, "appliance12.jpg"),
                });

                // запись в JSON-формате
                Serialize();
            } 

            // поместить данные для вывода 
            Appliances = _appliances;
        } // OnGet


        // вывод коллекции, упорядоченной по наименованию
        public void OnGetOrderByTitle() {
            Deserialize();
            Appliances = _appliances.OrderBy(a => a.Title).ToList();
        }


        // вывод коллекции, упорядоченной по убыванию цены
        public void OnGetOrderByPriceDesc() {
            Deserialize();
            Appliances = _appliances.OrderByDescending(a => a.Price).ToList();
        }


        // вывод коллекции, упорядоченной по возрастанию количества
        public void OnGetOrderByAmount() {
            Deserialize();
            Appliances = _appliances.OrderBy(a => a.Amount).ToList();
        }

        // словарь названий файлов
        public static Dictionary<string, string> FileNames { get; } = new()
        {
            ["Кондиционер"] = "appliance12.jpg",
            ["Фен"] = "appliance04.jpg",
            ["Холодильник"] = "appliance09.jpg",
            ["Стиральная машина"] = "appliance01.jpg",
            ["Духовой шкаф"] = "appliance02.jpg",
            ["Мультиварка"] = "appliance08.jpg"
        };

        // список наименования техники
        public SelectList NameAppliance { get; } = new SelectList(FileNames.Select(f => f.Key));


        [BindProperty] // Объект для ввода данных новой техники       
        public Appliance Appliance { get; set; } = new ("Кондиционер", "Крупная быт.техника", "OC9524", 26400, 5, "appliance12.jpg");

        public void OnGetAddAppliance()
        {
            // установка файла по типу прибора
            Appliance.Image = FileNames[Appliance.Title];

            _appliances.Insert(0, Appliance);
            Serialize();

            Appliances = _appliances;
        }


        #region Запись и чтение коллекции
        // чтение коллекции из файла в формате JSON 
        private void Deserialize()
        {
            string json = System.IO.File.ReadAllText(_path);
            _appliances = JsonConvert.DeserializeObject<List<Appliance>>(json, new JsonSerializerSettings
            {
                TypeNameHandling = TypeNameHandling.Objects,
            })!;
        } // Deseizlize

        // запись коллекции в файл в формате JSON
        public void Serialize()
        {
            // запись в JSON-формате
            string json = JsonConvert.SerializeObject(_appliances);
            System.IO.File.WriteAllText(_path, json);
        } // Serialize
        #endregion

    } // class AppliancesModel
}
