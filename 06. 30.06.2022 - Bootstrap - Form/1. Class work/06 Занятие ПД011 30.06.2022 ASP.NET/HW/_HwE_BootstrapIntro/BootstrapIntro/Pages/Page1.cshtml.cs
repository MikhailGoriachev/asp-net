using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using BootstrapIntro.Models.Task01;
using BootstrapIntro.Models.Task02;
using Newtonsoft.Json;

namespace BootstrapIntro.Pages
{
    // Страница 1.
    // Создайте коллекцию данных о бытовой технике на оптовом складе
    // (наименование, тип техники, артикул, цена, количество, изображение).
    // В коллекции должно быть не менее 12 элементов. Коллекцию инициируйте
    // набором записей, храните коллекцию в файле формата JSON в папке
    // App_Data проекта.
    //
    // Отображение данных одного элемента бытовой техники выполняйте при
    // помощи карточки, стилизованной соответствующими классами Bootstrap.
    //
    // По кнопкам-ссылкам, размещенным на странице вызывайте обработчики
    // запросов GET:
    //     • вывод коллекции, упорядоченной по наименованию
    //     • вывод коллекции, упорядоченной по убыванию цены
    //     • вывод коллекции, упорядоченной по возрастанию количества
    // 
    public class Page1Model : PageModel
    {
        // коллекция бытовых товаров
        private IEnumerable<Goods> _goods = new List<Goods>();

        // ссылка для вывода коллекции
        public IEnumerable<Goods>? DisplayGoods;

        // полное имя файла для записи/чтения
        private string _path = $"{Environment.CurrentDirectory}/App_Data/goods.json";

        // GET-запрос страницы
        public void OnGet() {
            // если файл есть - читаем данные из файла в коллекцию
            // если файла нет - сформирвоать коллекцию и записать данные
            // в файл

            if (System.IO.File.Exists(_path)) {
                Deserialize();
            } else  {
                // формирование коллекции
                _goods = new List<Goods> {
                    new ("микроволновая печь", "DEXP MS-70", "4726172", 6379, 12, "microwave_oven.jpg"),
                    new ("пылесос-робот", "Rekam RVL-1670G", "8150067", 5799, 5, "robot_vc.jpg"),
                    new ("блинница погружная", "DEXP BLN-3", "1385289", 1884, 6, "pan_cacker.jpg"),
                    new ("пылесос-робот", "ELAR1 Smartbot Brush", "4721104", 11119, 7, "robot_vc.jpg"),
                    new ("холодильник с морозильником", "DEXP RE-TD160NMA/W", "1319870", 22399, 5, "fridge.jpg"),
                    new ("микроволновая печь", "Gorenje MO17ELS", "8199146", 6699, 3, "microwave_oven.jpg"),
                    new ("стиральная машина", "BEKO WSRE612Z-SW", "1118440", 35099, 3, "wash_machine.jpg"),
                    new ("пылесос-робот", "Scarlett SC-VC80R12", "5313977", 8989, 6, "robot_vc.jpg"),
                    new ("блинница", "Maxwell MV-19HBN", "1098490", 3334, 5, "pan_cacker.jpg"),
                    new ("микроволновая печь", "Winia KOR-7707SW", "8199146", 8264, 2, "microwave_oven.jpg"),
                    new ("холодильник компактный", "Бирюса М70", "8116403", 15959, 3, "fridge.jpg"),
                    new ("стиральная машина", "Бирюса WM-HB712/10", "5327321", 35099, 3, "wash_machine.jpg"),
                    new ("блинница", "Tefal PY-303635", "0174095", 6184, 4, "pan_cacker.jpg"),
                    new ("микроволновая печь", "Hisense H20MOWS1H", "5330121", 7249, 7, "microwave_oven.jpg"),
                };

                // запись в JSON-формате
                Serialize();
            } // if

            // поместить ссылку на коллекцию в поле для вывода и установить 
            // акттвную кнопку в навигационном меню страницы
            DisplayGoods = _goods;
            ViewData["Mode"] = "source";
        } // OnGet


        // вывод коллекции, упорядоченной по заданию
        public void OnGetOrderBy(string target) {
            // чтение коллекции из файла
            Deserialize();

            // упорядочивание коллекции по заданию
            switch (target) {
                // упорядочивание по наименованию
                case "name":
                    _goods = _goods.OrderBy(g => g.Name);
                    ViewData["Mode"] = "name";
                    break;
                // упорядочивание по убыванию цены
                case "price":
                    _goods = _goods.OrderByDescending(g => g.Price);
                    ViewData["Mode"] = "price";
                    break;
                // упорядоченной по возрастанию количества
                case "quantity":
                    _goods = _goods.OrderBy(g => g.Quantity);
                    ViewData["Mode"] = "quantity";
                    break;
            } // switch

            // вывод упорядоченной коллекции 
            DisplayGoods = _goods;
        } // OnGetOrderBy

        // ----------------------------------------------------------------------
        // чтение коллекции из файла в  формате JSON (используем NewtonSoft)
        private void Deserialize() {
            string json = System.IO.File.ReadAllText(_path);
            _goods = JsonConvert.DeserializeObject<List<Goods>>(json)!;
        } // Deseizlize

        // запись коллекции в файл в формате JSON (используем NewtonSoft)
        private void Serialize() {
            string json = JsonConvert.SerializeObject(_goods);
            System.IO.File.WriteAllText(_path, json);
        } // Serialize
    } // class Page1Model
}
