using UiHintDisplay.Models;

namespace UiHintDisplay.Infrastructure;

// вспомогательные методы для приложения
public static class Utils
{
    // объект для получения случайных чисел
    public static readonly Random Random = new Random(Environment.TickCount);

    // Получение случайного числа
    public static int GetRandom(int lo, int hi) => Random.Next(lo, hi + 1);
    public static double GetRandom(double lo, double hi) => lo + (hi - lo) * Random.NextDouble();


    // инициализация коллекции сведений об инструкторах туристической фирмы
    public static List<Instructor> InitializeInstructors() => new List<Instructor>(new[] {
        new Instructor(1, "Панченко", "Любовь",    "Михайловна", new DateTime(1992, 12, 16), "A"),
        new Instructor(2, "Яйло",     "Александр", "Степанович", new DateTime(1988,  8,  8), "B"),
        new Instructor(3, "Хомченко", "Алексей",   "Кириллович", new DateTime(1995, 11, 10), "A"),
        new Instructor(4, "Чирик",    "Жанна",     "Эсмировна",  new DateTime(1990,  6,  1), "C"),
        new Instructor(5, "Ковтун",   "Ольга",     "Викторовна", new DateTime(1997,  3, 18), "C"),
    });
	

    // инициализация коллекции сведений о маршрутах, организованных туристической фирмой
    public static List<TouristicRoute> InitializeTouristicRoutes() => new List<TouristicRoute>(new [] {
        new TouristicRoute(  1, "Моспино",     "Новый Свет",    "Амвросиевка", 60, "A",  1 ),
        new TouristicRoute(  2, "Старобешево", "Ларино",        "Иловайск",    80, "A",  1 ),
        new TouristicRoute(  3, "Моспино",     "Коминтерново",  "Обрыв",      130, "B+", 3 ),
        new TouristicRoute(  4, "Иловайск",    "Новый Свет",    "Седово",     140, "C",  3 ),
        new TouristicRoute(  5, "Дебальцево",  "Амвросиевка",   "Снежное",     90, "B+", 3 ),
        new TouristicRoute(  6, "Енакиево",    "Зугрэс",        "Снежное",     75, "A+", 4 ),
        new TouristicRoute(  7, "Харцызск",    "Новый Свет",    "Обрыв",      160, "C+", 4 ),
        new TouristicRoute(  8, "Дебальцево",  "Коминтерново",  "Седово",     140, "C+", 4 ),
        new TouristicRoute(  9, "Дебальцево",  "Новый Свет",    "Мангуш",     180, "C+", 5 ),
        new TouristicRoute( 10, "Снежное",     "Моспино",       "Енакиево",    80, "B",  5 ),
        new TouristicRoute( 11, "Седово",      "Коминтерново",  "Снежное",    160, "C-", 5 ),
        new TouristicRoute( 12, "Иловайск",    "Моспино",       "Старобешево", 80, "B-", 2 ),
    });
} // class Utils

