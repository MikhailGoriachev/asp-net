using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using HomeWork.Models;
using HomeWork.Models.ViewModels;
using HomeWork.Models.ViewModels.Routes;
using Route = HomeWork.Models.Route;

namespace HomeWork.Infrastructure;

// Класс Утилиты
public static class Utils
{
    // объект для получения случайных числовых значений
    private static Random _rand;

    public static Random Rand
    {
        get => _rand;
        set => _rand = value;
    }


    // коллекция ФИО
    private static List<(string surname, string name, string patronymic)>? _fullNameCollection;

    public static List<(string surname, string name, string patronymic)> FullNameCollection =>
        _fullNameCollection ??= new()
        {
            ("Игнатов", "Лев", "Протасьевич"),
            ("Афанасьев", "Тихон", "Ефимович"),
            ("Матвеев", "Константин", "Оскарович"),
            ("Михеев", "Абрам", "Романович"),
            ("Кононов", "Влас", "Витальевич"),
            ("Одинцов", "Адам", "Еремеевич"),
            ("Буров", "Модест", "Матвеевич"),
            ("Князев", "Аверьян", "Еремеевич"),
            ("Емельянов", "Лука", "Святославович"),
            ("Захаров", "Тарас", "Валерьевич"),
            ("Дмитриева", "Фрида", "Юрьевна"),
            ("Федосеева", "Аурелия", "Геннадьевна"),
            ("Филиппова", "Алира", "Семёновна"),
            ("Рожкова", "Ляля", "Лаврентьевна"),
            ("Васильева", "Алла", "Семеновна"),
            ("Белоусова", "Христина", "Степановна"),
            ("Мартынова", "Любовь", "Пантелеймоновна"),
            ("Попова", "Ирэна", "Донатовна"),
            ("Сазонова", "Эдуарда", "Пантелеймоновна"),
            ("Григорьева", "Ева", "Семёновна"),
        };


    // коллекция уровней сложности маршрута
    private static List<string>? _difficultList;

    public static List<string> DifficultList => _difficultList ??= new()
    {
        "A-", "A", "A+", "B-", "B", "B+", "C-", "C", "C+"
    };


    // коллекция категорий инструктора
    private static List<string>? _categories;

    public static List<string> Categories => _categories ??= new() { "A", "B", "C" };


    // данные маршрута
    private static List<(string startPoint, string middlePoint, string endpoint, int length)>? _routesData;

    public static List<(string startPoint, string middlePoint, string endPoint, int length)> RoutesData =>
        _routesData ??= new()
        {
            ("Херсон", "Олешковские пески", "Аскания-Нова", 167),
            ("Ужгород", "Невицкое", "Карпаты", 80),
            ("Батурин", "Тростянец", "Качановка", 143),
            ("Винница", "Тульчин", "Буша", 90),
            ("Урыч", "Розгирче", "Бубнище", 100),
            ("Каменец-Подольский", "Кривче", "Хотин", 55),
            ("Черновцы", "Залещики", "Нырков", 55),
            ("Киев", "Буки", "Умань", 80),
            ("Коростень", "Новоград-Волынский", "Корец", 30),
            ("Луцк", "Дубно", "Клевань", 70),
        };


    // пункты для маршрута
    private static List<string>? _points;

    public static List<string> Points => _points ??= new()
    {
        "Херсон",
        "Олешковские пески",
        "Нова",
        "Ужгород",
        "Невицкое",
        "Карпаты",
        "Батурин",
        "Тростянец",
        "Качановка",
        "Винница",
        "Тульчин",
        "Буша",
        "Урыч",
        "Розгирче",
        "Бубнище",
        "Подольский",
        "Кривче",
        "Хотин",
        "Черновцы",
        "Залещики",
        "Нырков",
        "Киев",
        "Буки",
        "Умань",
        "Коростень",
        "Волынский",
        "Корец",
        "Луцк",
        "Дубно",
        "Клевань",
    };


    // список уровней сложности и ценности
    private static Dictionary<string, int>? _difficultyListLevel;

    public static Dictionary<string, int> DifficultyListLevel =>
        _difficultyListLevel ??= new()
        {
            ["A-"] = 0,
            ["A"] = 1,
            ["A+"] = 2,
            ["B-"] = 3,
            ["B"] = 4,
            ["B+"] = 5,
            ["C-"] = 6,
            ["C"] = 7,
            ["C+"] = 8,
        };


    // клиенты для заполнения
    private static List<Client>? _clients;

    public static List<Client> Clients => _clients ??= new()
    {
        new Client("Михайлов", "Аверкий", "Онисимович", GetInt(20, 35), "+380-66-365-06-05", "mikhailov@gmail.com",
            "mikhailov133", true, "male_001.jpg"),
        new Client("Агафонов", "Дональд", "Никитевич", GetInt(20, 35), "+380-50-145-76-12", "agaonov@gmail.com",
            "agaonov853", true, "male_002.jpg"),
        new Client("Шашков", "Аристарх", "Геннадиевич", GetInt(20, 35), "+380-99-151-65-86", "shashkov@mail.ru",
            "shashkov784", false, "male_003.jpg"),
        new Client("Сафонов", "Денис", "Андреевич", GetInt(20, 35), "+380-99-571-62-12", "safonov@gmail.com",
            "safonov943", true, "male_004.jpg"),
        new Client("Панов", "Самуил", "Евгеньевич", GetInt(20, 35), "+380-50-152-22-89", "panov@outlook.com",
            "panov542", true, "male_005.jpg"),
        new Client("Горбунов", "Соломон", "Анатольевич", GetInt(20, 35), "+380-50-954-12-75", "gorbunov@outlook.com",
            "gorbunov652", false, "male_006.jpg"),
        new Client("Прохоров", "Модест", "Тимурович", GetInt(20, 35), "+380-66-132-32-62", "prohorov@gmail.com",
            "prohorov741", true, "male_007.jpg"),
        new Client("Гришин", "Агафон", "Созонович", GetInt(20, 35), "+380-66-774-21-86", "grishin@mail.ru",
            "grishin653", true, "male_008.jpg"),
        new Client("Меркушев", "Власий", "Владленович", GetInt(20, 35), "+380-50-954-36-22", "merkushev@gmail.com",
            "merkushev763", false, "male_009.jpg"),
        new Client("Баранов", "Казимир", "Григорьевич", GetInt(20, 35), "+380-99-442-70-21", "baranov@gmail.com",
            "baranov787", true, "male_010.jpg"),
        new Client("Суворова", "Диодора", "Викторовна", GetInt(20, 35), "+380-99-945-38-79", "suvorova@gmail.com",
            "suvorova765", false, "female_001.jpg"),
        new Client("Некрасова", "Вера", "Денисовна", GetInt(20, 35), "+380-66-212-17-61", "nekrasova@outlook.com",
            "nekrasova953", true, "female_002.jpg"),
    };

    #region Конструкторы

    // статический конструктор
    static Utils()
    {
        // установка значений
        _rand = new Random();
    }

    #endregion

    #region Утилиты

    // получение случайного целого числа
    public static int GetInt(int min, int max) => _rand.Next(min, max);


    // получение слуйчного дробного числа
    public static double GetDouble(double min, double max)
    {
        // если границы диапазона неверны
        if (min >= max)
            throw new InvalidDataException($"Utils.GetDouble: Указан непрваильный диапазон ({min} - {max})");

        double value;

        do
        {
            value = GetInt((int)min, (int)max - 1) + _rand.NextDouble();
        } while (value <= min && value >= max);

        return value;
    }


    // генератор случайных дат
    public static DateTime GetDate(DateTime minDate, DateTime maxDate)
    {
        // разница в днях между датами
        int days = (int)(maxDate - minDate).TotalDays;

        return maxDate.AddDays(GetInt(0, days));
    }


    // сохранить файл на сервер
    public static void SaveFile(string path, IFormFile file)
    {
        using Stream stream = File.Create(Path.Combine(path, file.FileName));

        file.CopyTo(stream);
    }


    // генерация инструктора
    public static Instructor GetInstructor()
    {
        // получить ФИО
        var fullName = FullNameCollection[GetInt(0, FullNameCollection.Count)];

        return new Instructor(fullName.surname, fullName.name, fullName.patronymic,
            GetDate(new DateTime(1970, 1, 1), new DateTime(1990, 1, 1)),
            Categories[GetInt(0, Categories.Count)]);
    }

    // генерация маршрута
    public static Route GetRoute(List<Instructor> instructors)
    {
        // данные о маршруте
        var dataRoute = RoutesData[GetInt(0, RoutesData.Count)];

        return new Route(dataRoute.startPoint,
            dataRoute.middlePoint,
            dataRoute.endPoint,
            dataRoute.length,
            DifficultList[GetInt(0, DifficultList.Count)],
            instructors[GetInt(0, instructors.Count)].Id
        );
    }

    // генерация данных маршрутов и инструкторов
    public static (IEnumerable<Instructor> instructors, IEnumerable<Route> routes) GetDataRoutes(int countInstructor,
        int countRoutes)
    {
        var instructors = Enumerable.Repeat(0, countInstructor).Select(i => GetInstructor()).ToList();

        return (instructors, Enumerable.Repeat(0, countRoutes).Select(r => GetRoute(instructors)));
    }

    // расширяющий метод для маршрутов, возвращает модель представления маршрута
    public static IEnumerable<RouteViewModel> GetViewModel(this IEnumerable<Route> routes,
        IEnumerable<Instructor> instructors)
        => routes.Join(instructors, r => r.InstructorId, i => i.Id,
            (r, i) => new RouteViewModel(r, i));


    // зашифровать пароль
    public static string Crypt(string password)
    {
        var bytes = Encoding.UTF8.GetBytes(password).Select(c => (byte)(c ^ 42));

        return Encoding.UTF8.GetString(bytes.ToArray());
    }

    #endregion
}
