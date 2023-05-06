using System;
using System.Text;
using System.Threading;
using Home_work.Models;
using Home_work.Models.Figures;

namespace Home_work.Utilities;

public static class Utils
{
    
    // объект для получения случайных чисел
    public static Random Random = new Random();
    
    // формирование случайных чисел в диапазоне от lo до hi
    public static double GetRandom(double lo, double hi)
        => lo + (hi - lo)*Random.NextDouble();
    public static int GetRandom(int lo, int hi)
        => Random.Next(lo,hi);


    #region Книги
    public static IEnumerable<Book> GetBooks()
    {
        //Список файлов-обложек для книг
        List<string> files = Directory.GetFiles($"{Environment.CurrentDirectory}\\wwwroot\\images\\covers")
                        .Select(f => f.Substring(f.LastIndexOf('\\')+1))
                        .Where(f => f.ToLower().Contains("cover_"))
                        .ToList();

        return new List<Book>()
            {
                new(1,"Грокаем алгоритмы" ,"Адитья Бхаргава" ,$"{files[0]}" ,                          GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(2,"Предметно-ориентированное проектирование (DDD)" ,"Эрик Эванс" ,$"{files[1]}" ,  GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(3,"Искусство программирования" ,"Дональд Кнут" ,$"{files[2]}" ,                    GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(4,"Путь программиста" ,"Джон Сонмез" ,$"{files[3]}" ,                              GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(5,"Эффективная работа с унаследованным кодом" ,"Майкл Физерс" ,$"{files[4]}" ,     GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(6,"Head First. Паттерны проектирования" ,"Эрик Фримен" ,$"{files[5]}" ,            GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(7,"Рефакторинг" ,"Мартин Фаулер" ,$"{files[6]}" ,                                  GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(8,"Совершенный код" ,"Стив Макконнелл" ,$"{files[7]}" ,                            GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(9,"Чистый код" ,"Роберт Мартин" ,$"{files[8]}" ,                                   GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(10,"Паттерны объектно-ориентированного проектирования","Эрих Гамма",$"{files[9]}",GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
            };

        /*return new List<Book>()
            {
                new(1,"Грокаем алгоритмы" ,"Адитья Бхаргава" ,"Cover_1.jpg" ,                          GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(2,"Предметно-ориентированное проектирование (DDD)" ,"Эрик Эванс" ,"Cover_2.jpg" ,  GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(3,"Искусство программирования" ,"Дональд Кнут" ,"Cover_3.jpg" ,                    GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(4,"Путь программиста" ,"Джон Сонмез" ,"Cover_4.jpg" ,                              GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(5,"Эффективная работа с унаследованным кодом" ,"Майкл Физерс" ,"Cover_5.jpg" ,     GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(6,"Head First. Паттерны проектирования" ,"Эрик Фримен" ,"Cover_6.jpg" ,            GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(7,"Рефакторинг" ,"Мартин Фаулер" ,"Cover_7.jpg" ,                                  GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(8,"Совершенный код" ,"Стив Макконнелл" ,"Cover_8.jpg" ,                            GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(9,"Чистый код" ,"Роберт Мартин" ,"Cover_9.jpg" ,                                   GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
                new(10,"Паттерны объектно-ориентированного проектирования","Эрих Гамма","Cover_10.jpg",GetRandom(2009,2013), GetRandom(1,6), GetRandom(1500,3000)),
            };*/
    }
    #endregion


    #region Фиугры
    //Генерация фигуры
    public static IFigure GetFigure(int id)
    {
        IFigure[] figures =
        {

                new Rectangle(GetRandom(5d,11d),GetRandom(5d,11d)),
                new Triangle(GetRandom(5d,11d),GetRandom(5d,11d),GetRandom(5d,11d)),
                new Square(GetRandom(5d,11d))
        };

        IFigure figure = figures[GetRandom(0, figures.Length)];
        /*figure.Id = LastFigureId++;*/
        figure.Id = id;
        return figure;
    }

    //Словарь изображений
    public static Dictionary<string, string> FiguresIcons = new Dictionary<string, string>()
        {
            {"Прямоугольник","Rectangle.jpg"},
            {"Квадрат","Square.jpg"},
            {"Треугольник","Triangle.png"},
        };


    public static int LastFigureId = 1;

    //Фигура для добавления из формы
    public static IFigure FormFigure = new Square(0);

    #endregion

} // class Utils
