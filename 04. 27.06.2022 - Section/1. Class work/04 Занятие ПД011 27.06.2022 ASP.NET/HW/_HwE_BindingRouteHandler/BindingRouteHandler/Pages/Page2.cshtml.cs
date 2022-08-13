using BindingRouteHandler.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace BindingRouteHandler.Pages
{
    // Страница 2.
    // Создайте коллекцию данных о бытовой технике на оптовом складе
    // (наименование, тип техники, артикул, цена, количество, изображение).
    // В коллекции должно быть не менее 12 элементов. Коллекцию инициируйте
    // набором записей, хранить коллекцию в файле не требуется. По ссылкам,
    // размещенным на странице вызывайте обработчики запросов GET:
    //     • вывод коллекции, упорядоченной по наименованию
    //     • вывод коллекции, упорядоченной по убыванию цены
    //     • вывод коллекции, упорядоченной по возрастанию количества
    // В форме задайте диапазон цены, по кнопке submit выводите часть
    // коллекции – товары с ценой, попадающей в заданный диапазон. 
    // 
    public class Page2Model : PageModel
    {
        // коллекция бытовых товаров
        private IEnumerable<Goods> _goods = new List<Goods> {
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

        // ссылка для вывода коллекции
        public IEnumerable<Goods>? DisplayGoods;


        public void OnGet() {
        } // OnGet


        // вывод коллекции, упорядоченной по заданию
        public void OnGetOrderBy(string target) {
            _goods = target switch {
                // упорядочивание по наименованию
                "name" => _goods.OrderBy(g => g.Name),
                
                // упорядочивание по убыванию цены
                "price" => _goods.OrderByDescending(g => g.Price),
                
                // упорядоченной по возрастанию количества
                "quantity" => _goods.OrderBy(g => g.Price),
                _ => _goods
            };

            // вывод упорядоченной коллекции 
            DisplayGoods = _goods;
        } // OnGetOrderBy


        // В форме задайте диапазон цены, по кнопке submit выводите часть
        // коллекции – товары с ценой, попадающей в заданный диапазон. 
        public void OnPost(int low, int high) {
            // выборка данных и ее вывод
            DisplayGoods = _goods.Where(g => low <= g.Price && g.Price <= high);
        } // OnPost
    } // class Page2Model
}
