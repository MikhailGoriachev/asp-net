using Microsoft.AspNetCore.Mvc;
using Home_work.Models;
using Home_work.Infrastructure;
using Newtonsoft.Json;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace Home_work.Controllers
{
    public class InstructorsController : Controller
    {
        private IEnumerable<Instructor> _instructors;

        //Путь к файлу
        public string FilePath
        {
            get;
            set;
        }


        //Сортировки 
        public Dictionary<string, (string title, Func<IEnumerable<Instructor>> func)> SortFuncts { get; set; }

        public InstructorsController()
        {
            FilePath = $"{Environment.CurrentDirectory}\\App_Data\\instructors.json";

            //Если файл есть - десериализация, в противном случае генерация
            if (System.IO.File.Exists(FilePath))
            {
                Desiralize();
            }
            else
            {
                _instructors = Utils.GetInstructors();
                Serialize();
            }

            SortFuncts = new()
            {
                ["OrderByCategory"] = ("Сортировка по убыванию категорий",() => {

                    Dictionary<string, int> Weight = new()
                    {
                        ["A"] = 0,
                        ["B"] = 1,
                        ["C"] = 2,
                    };

                    return _instructors?.OrderByDescending(i => Weight[i.Category]);
                    }),
                ["OrderBySurnames"] = ("Сортировка в алфавитном порядке",() => _instructors?.OrderBy(i => i.Surname)),

                ["Default"] = ("Исходная коллекция",() => _instructors),
            };

        }


        public IActionResult Default()
        {
            //Выводится ли сейчас исходная коллекция
            ViewBag.IsDefault = true;

            return View("Instructors",_instructors);
        }

        #region CRUD

        //Редактирование
        public IActionResult EditInstructor(int id)
        {
            Instructor? instructor = _instructors.FirstOrDefault(b => b.Id == id);

            //Если элемент не найден, то форму не запускаем
            if (instructor is null)
                return View("Instructors", _instructors);


            //Категории иснтрукторов
            ViewBag.Categories = new SelectList(Utils.Categories);

            //Форма редактирования объекта
            return View("InstructorForm", instructor);
        }

        //Post-обработчик формы на редактирование и добавление 
        [HttpPost]
        public IActionResult FormSubmit(Instructor model)
        {

            Instructor instructor = _instructors.FirstOrDefault(r => r.Id == model.Id);

            if (instructor is null)
                return View("Instructors", _instructors);

            //Нахождение и изменение элемента
            var list = _instructors.ToList();

            list[list.FindIndex(i => i.Id == model.Id)] = model;

            _instructors = list;

            Serialize();

            //Маршруты
            string routesFile = $"{Environment.CurrentDirectory}\\App_Data\\routes.json";
            List<Models.Route> routes = Utils.Desiralize<List<Models.Route>>(routesFile);

            //Находим нужный маршрут с нужным инструктором и меняем инструктора в маршруте
            if (routes.Find(r => r.InstructorData.Id == model.Id) != null){

                var foundRoutes = routes.FindAll(r => r.InstructorData.Id == model.Id);

                foundRoutes.ForEach(fr => routes[routes.FindIndex(r => r.Id == fr.Id)].InstructorData = model);

                /*routes[routes.FindIndex(r => r.InstructorData.Id == model.Id)].InstructorData = model;*/

                //Сереализация списка маршрутов с изменёнными зписями
                Utils.Serialize(routes, routesFile);

            }

            ViewBag.IsDefault = true;

            //Если добавление - сортируем коллекцию по Id
            return View("Instructors", _instructors);
        }

        #endregion

        #region Сортировки и выборки

        //Инструкторы по убыванию категорий
        public IActionResult OrderBy(string value)
        {
            (string header, Func<IEnumerable<Instructor>> function) tuple = SortFuncts.ContainsKey(value) ? SortFuncts[value] : SortFuncts["Default"];

            ViewBag.Header = tuple.header;

            return View("Instructors", tuple.function());
        }

        //Выборки
        [HttpPost]
        public IActionResult SelectByCategory(string category)
        {
            return View("Instructors",_instructors.Where(i => i.Category.ToLower() == category.ToLower()));
        }

        #endregion


        #region JSON

        public IActionResult DeleteFile()
        {
            if (System.IO.File.Exists(FilePath))
                System.IO.File.Delete(FilePath);

            return View("Instructors", _instructors);
        }

        //Сериализация 
        public void Serialize()
        {
            string json = JsonConvert.SerializeObject(_instructors, Formatting.Indented);

            //Получение имени папки
            string directory = FilePath.Replace(FilePath.Substring(FilePath.LastIndexOf('\\')), "");

            //Есил каталога нет, то создать 
            if (!Directory.Exists(directory))
                Directory.CreateDirectory(directory);

            System.IO.File.WriteAllText(FilePath, json);
        }

        //Десериализация
        public void Desiralize()
        {
            string json = System.IO.File.ReadAllText(FilePath);

            //Если десериализация прошла не успешно тогда просто сгенерировать новую коллекцию
            _instructors = JsonConvert.DeserializeObject<List<Instructor>>(json) ?? Utils.GetInstructors().ToList();
        }

        #endregion


        //AJAX запрос
        public IActionResult GetInstructor(int id)
        {
            Instructor instructor = _instructors.FirstOrDefault(i => i.Id == id);

            if (instructor == null)
                return Json(new Instructor());

            return Json(instructor);
        }

    }
}
