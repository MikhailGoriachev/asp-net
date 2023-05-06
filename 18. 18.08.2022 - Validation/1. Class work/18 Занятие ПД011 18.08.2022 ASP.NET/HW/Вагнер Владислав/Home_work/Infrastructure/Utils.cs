using System;
using System.Text;
using System.Threading;
using Home_work.Models;
using Newtonsoft.Json;

namespace Home_work.Infrastructure;

public static class Utils
{

    // объект для получения случайных чисел
    public static Random Random = new Random();

    // формирование случайных чисел в диапазоне от lo до hi
    public static double GetRandom(double lo, double hi)
        => lo + (hi - lo) * Random.NextDouble();
    public static int GetRandom(int lo, int hi)
        => Random.Next(lo, hi);

    //Категории инструкторов
    public static string[] Categories =
    {
        "A",
        "B",
        "C",
    };

    //Инструкторы
    public static Instructor[] GetInstructors()
    {
        int length = Categories.Length;
        return new[]{
            new Instructor (1, "Исаев", "Алексей", "Владимирович",    Categories[GetRandom(0,length)], new DateTime(1992, 8, 12)),
            new Instructor (2, "Кротов", "Евгений", "Вениаминович",   Categories[GetRandom(0,length)], new DateTime(1988, 12, 23)),
            new Instructor (3, "Островский", "Назар", "Александрович",Categories[GetRandom(0,length)], new DateTime(1997, 7, 8)),
            new Instructor (4, "Сочинская", "Ольга", "Алексеевна",     Categories[GetRandom(0,length)], new DateTime(1998, 4, 17)),
            new Instructor (5, "Астахов", "Борис", "Филиппович",      Categories[GetRandom(0,length)], new DateTime(1994, 2, 21)),
        };
    }

    //Категории инструкторов
    public static string[] Complexity =
    {
        "A",
        "A",
        "A+",
        "B-",
        "B",
        "B+",
        "C-",
        "C",
        "C+",
    };

    //Пункты назначений 
    public static string[] Points = {
        "Варшава",
        "Прага",
        "Помпеи",
        "Будапешт",
        "Марсель",
        "Майнхед",
        "Селангер",
        "Тронхейме",};

    //Генерация маршрутов
    public static List<Models.Route> GetRoutes(string InstructorsFile = "")
    {
        int length = Complexity.Length;

        //Получение списка инструкторов
        List<Instructor> instructors = new List<Instructor>();

        if (InstructorsFile.Contains("json"))
            instructors = Desiralize<List<Instructor>>(InstructorsFile);

        //Если десериализовать файл не удалось, тогда используем инструктроров по умолчанию
        if (instructors.Count<=0)
            instructors = GetInstructors().ToList();

        List<Models.Route> routes = new List<Models.Route>();


        SortedSet<string> routeSet = new SortedSet<string>();

        //Инициализация списка
        for (int i = 0; i < 5; i++)
        {
            routeSet.Clear();

            //Добавление 3-х уникальных пунктов
            while (routeSet.Count != 3)
                routeSet.Add(Points[GetRandom(0, Points.Length)]);

            routes.Add(
                new(i+1, routeSet.ToList()[0], routeSet.ToList()[1], routeSet.ToList()[2], Complexity[GetRandom(0, length)], GetRandom(40, 300), instructors[GetRandom(0, instructors.Count)]));
        }

        return routes;
    }

    #region Сереализация/Десериализация

    //Сериализация 
    public static void Serialize(Object obj,string filePath)
    {
        string json = JsonConvert.SerializeObject(obj, Formatting.Indented);

        //Получение имени папки
        string directory = filePath.Replace(filePath.Substring(filePath.LastIndexOf('\\')), "");

        //Есил каталога нет, то создать 
        if (!Directory.Exists(directory))
            Directory.CreateDirectory(directory);

        File.WriteAllText(filePath, json);
    }

    //Десериализация
    public static T Desiralize<T>(string filePath) where T : new()
    {
        if (!File.Exists(filePath))
            return new();

        string json = System.IO.File.ReadAllText(filePath);

        //Если десериализация прошла не успешно тогда просто сгенерировать новую коллекцию
        return JsonConvert.DeserializeObject<T>(json);
    }

    #endregion

} // class Utils
