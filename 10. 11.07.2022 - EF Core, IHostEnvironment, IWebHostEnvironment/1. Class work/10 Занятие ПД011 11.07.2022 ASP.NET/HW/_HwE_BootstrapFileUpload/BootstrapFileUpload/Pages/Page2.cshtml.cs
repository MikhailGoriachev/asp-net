using BootstrapFileUpload.Infrastructure;
using BootstrapFileUpload.Models.Task02;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;


namespace BootstrapFileUpload.Pages
{
    // Требуется хранить в файле формата JSON в папке App_Data проекта коллекцию
    // сведений о кораблях: тип, название, длина, ширина, водоизмещение, год
    // постройки, фото корабля. Инициализация коллекции сведений о кораблях
    // должна проводиться при отсутствии файла (формируйте не менее 10 записей). 
    // Функционал для разработки:
    //     • вывод всей коллекции в исходном порядке 
    //     • вывод коллекции, упорядоченной по возрастанию года изготовления
    //     • вывод коллекции, упорядоченной по убыванию водоизмещения
    //     • вывод коллекции, упорядоченной по названиям кораблей
    //     • добавление корабля в коллекцию (естественно, с сохранением измененной
    //       коллекции в файле)
    // 

    [IgnoreAntiforgeryToken]
    public class Page2Model : PageModel
    {
        // модель хранилища данных
        private List<Ship> _fleet = new List<Ship>();

        // свойство для PageModel
        public IEnumerable<Ship>? DisplayFleet;

        // объект для ввода характеристик из формы
        [BindProperty]
        public Ship Ship { get; set; } = new Ship();

        // полное имя файла для записи/чтения
        private string _path = $"{Environment.CurrentDirectory}/App_Data/Task02/fleet.json";
        

        // сообщение об ошибке, для простой "валидации"
        public string ErrorMsg { get; private set; } = "";

        // обработчик GET-запроса к странице
        public void OnGet() {
            // если файл есть - читаем данные из файла в коллекцию
            // если файла нет - сформировать коллекцию и записать данные
            // в файл
            // чтение из файла в формате JSON
            if (System.IO.File.Exists(_path)) {
               Deserialize();
            } else {
                // формирование коллекции
                _fleet = new List<Ship>(new [] {
                    new Ship("линейный корабль", "Виктория", 69.34, 15.8, 3556, 1765, "victory.jpg"),
                    new Ship("линейный корабль", "Три святителя", 63.4, 17.3, 4700, 1838, "tri_sviatitelia.jpg"),
                    new Ship("чайный клиппер", "Катти Сарк", 85.4, 10.97, 2133, 1869, "cuttysark.jpg"),
                    new Ship("чайный клиппер", "Фермопилы", 64.7, 11.0, 991, 1869, "thermopylae.jpg"),
                    new Ship("барк", "Крузенштерн", 114.5, 14, 1645, 1926, "kruzenstern.jpg"),
                    new Ship("барк", "Товарищ", 88.0, 12.7, 4750, 1892, "camrad.jpg"),
                });

                // запись в JSON-формате
                Serialize();
            } // if

            // Поместить данные в переменную для вывода на страницу
            // установить признак активной кнопки на странице
            DisplayFleet =  _fleet;
        } // OnGet

        // Обработка формы ввода параметров добавляемого корабля
        public void OnPost() {
            // читаем коллекцию из файла
            Deserialize();

            // задать имя файла с изображением товара

            // добавить товар в коллекцию и сохранить коллекцию
            _fleet.Add(Ship);
            Serialize();

            // вывод обновленной коллекции
            DisplayFleet = _fleet;
        } // OnPost


        // ----------------------------------------------------------------------
        // чтение коллекции из файла в  формате JSON (используем NewtonSoft)
        private void Deserialize() {
            string json = System.IO.File.ReadAllText(_path);
            _fleet = JsonConvert.DeserializeObject<List<Ship>>(json)!;
        } // Deseizlize

        // запись коллекции в файл в формате JSON (используем NewtonSoft)
        private void Serialize() {
            string json = JsonConvert.SerializeObject(_fleet);
            System.IO.File.WriteAllText(_path, json);
        } // Serialize
    } // class Page3Model 
}
