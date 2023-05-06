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

    #region Маршруты

    //Категории инструкторов
    public static string[] Complexity =
    {
        "A-",
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
    public static List<Models.Routes.Route> GetRoutes(string InstructorsFile = "")
    {
        int length = Complexity.Length;

        //Получение списка инструкторов

        List<Instructor> instructors = new List<Instructor>();

        //Если имя файла корректное, тогда попытаться десериализовать 
        if (InstructorsFile.Contains("json"))
            instructors = Deserialize<List<Instructor>>(InstructorsFile);

        //Если десериализовать файл не удалось, тогда используем инструктроров по умолчанию
        if (instructors.Count <= 0)
            instructors = GetInstructors().ToList();

        List<Models.Routes.Route> routes = new List<Models.Routes.Route>();


        SortedSet<string> routeSet = new SortedSet<string>();

        //Инициализация списка
        for (int i = 0; i < 5; i++)
        {
            routeSet.Clear();

            //Добавление 3-х уникальных пунктов
            while (routeSet.Count != 3)
                routeSet.Add(Points[GetRandom(0, Points.Length)]);

            routes.Add(
                new(i + 1, routeSet.ToList()[0], routeSet.ToList()[1], routeSet.ToList()[2], Complexity[GetRandom(0, length)], GetRandom(40, 300), instructors[GetRandom(0, instructors.Count)]));
        }

        return routes;
    }

    #endregion

    //Клиенты 
    public static List<Client> GetClients()
    {
        return new List<Client>()
        {
            new Client(1, "Михалков","Алексей","Филиппович",  36,"+380 71 345 091", "user1@gmail.com", "12pass51","man_001.jpg",  GetRandom(0,2) == 1),
            new Client(2, "Пелых","Михаил","Созонович",       25,"+380 71 345 091", "user2@gmail.com", "12pass52","man_002.jpg",  GetRandom(0,2) == 1),
            new Client(3, "Карпов","Станислав","Кириллович",  39,"+380 71 334 145", "user3@gmail.com", "12pass53","man_003.jpg",  GetRandom(0,2) == 1),
            new Client(4, "Хавалджи","Мстислав ","Евгеньевич",57,"+380 71 897 091", "user4@gmail.com", "12pass54","man_004.jpg",  GetRandom(0,2) == 1),
            new Client(5, "Пархоменко","Алексей","Тимофеевич",31,"+380 50 345 555", "user5@gmail.com", "12pass55","man_005.jpg",  GetRandom(0,2) == 1),
            new Client(6, "Ковалёв","Авраам","Эдуардович",    41,"+380 71 897 345", "user6@gmail.com", "12pass56","man_006.jpg",  GetRandom(0,2) == 1),
            new Client(7, "Попов","Матвей","Никитевич",       47,"+380 66 345 765", "user7@gmail.com", "12pass57","man_007.jpg",  GetRandom(0,2) == 1),
            new Client(8, "Степанов","Станислав","Валерьевич",24,"+380 71 454 654", "user8@gmail.com", "12pass58","man_008.jpg",  GetRandom(0,2) == 1),
            new Client(9, "Блинова","Валентина","Мироновна",  32,"+380 71 815 345", "user9@gmail.com", "12pass59","woman_001.jpg",GetRandom(0,2) == 1),
            new Client(10,"Баранова","Дарья","Борисовна",     26,"+380 71 323 123", "user10@gmail.com","12pass60","woman_002.jpg",GetRandom(0,2) == 1),
            new Client(11,"Щукина","Богдана","Пётровна",      35,"+380 71 233 091", "user11@gmail.com","12pass61","woman_007.jpg",GetRandom(0,2) == 1),
            new Client(12,"Комаров","Август","Робертович",    40,"+380 71 097 091", "user12@gmail.com","12pass62","man_009.jpg",  GetRandom(0,2) == 1),
        };
    }

    /*private static byte[] key = BitConverter.GetBytes(42);*/

    //Закодировать/Декодировать строку 
    public static string EncodeString(string str)
    {
        byte[] strBytes = Encoding.UTF8.GetBytes(str);

        byte[] result = new byte[strBytes.Length];

        for (int i = 0; i < strBytes.Length; i++)
        {
            /*result[i] = (byte)(strBytes[i] ^ key[i % key.Length]);*/
            result[i] = (byte)(strBytes[i] ^ 42);
        }

        return Encoding.UTF8.GetString(result);
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
    public static T Deserialize<T>(string filePath) where T : new()
    {
        //Если файла не существует - создать объект по умолчанию (например пустой List<>)
        if (!File.Exists(filePath))
            return new();

        string json = File.ReadAllText(filePath);

        return JsonConvert.DeserializeObject<T>(json);
    }

    #endregion

    //Сохранение файла 
    public static bool SaveFile(string path, IFormFile formFile)
    {
        string fullPath = Path.Combine(path, formFile.FileName);

        try{
            //Запись файла по нужному пути
            using (FileStream stream = File.Create(fullPath))
            {
                formFile.CopyTo(stream);
            }
        } 
        catch (Exception ex){
            return false;
        }

        return true;

    }


} // class Utils
