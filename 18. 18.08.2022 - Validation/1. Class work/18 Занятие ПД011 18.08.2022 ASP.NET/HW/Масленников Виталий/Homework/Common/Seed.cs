using Homework.Models.TravelCompany;

namespace Homework.Common;

public static class Seed
{
    // Данные об инструкторах
    public static List<Instructor> Instructors() => new List<Instructor>
    {
        new () { Id =  1, Surname = "Юрковский",   Name = "Марк",      Patronymic = "Максимилианович",
            DoB = new DateTime(1965, 5, 12), Category = "A"}, 
        new () { Id =  2, Surname = "Якубовская",  Name = "Диана",     Patronymic = "Павловна",
            DoB = new DateTime(1965, 5, 12),   Category = "B"}, 
        new () { Id =  3, Surname = "Шапиро",      Name = "Федор",     Patronymic = "Федорович",
            DoB = new DateTime(1974, 4, 1),  Category = "C"}, 
        new () { Id =  4, Surname = "Вожжаев",     Name = "Сергей",    Patronymic = "Денисович",
            DoB = new DateTime(1984, 8, 20), Category = "A"}, 
        new () { Id =  5, Surname = "Хроменко",    Name = "Игорь",     Patronymic = "Владимирович",
            DoB = new DateTime(1979, 7, 17), Category = "B"}, 
    };

    // Данные о маршрутах
    public static List<Models.TravelCompany.Route> Routes() => new List<Models.TravelCompany.Route>()
    {
        new()
        {
            Id = 1, Length = Random.Shared.Next(2, 21), Difficulty = "A-",
            StartPoint = "Начальный пункт 1", MiddlePoint = "Промежуточный пункт 1",
            FinishPoint = "Конечный пункт 1", InstructorId = 1
        },
        new()
        {
            Id = 2, Length = Random.Shared.Next(2, 21), Difficulty = "B",
            StartPoint = "Начальный пункт 2", MiddlePoint = "Промежуточный пункт 2",
            FinishPoint = "Конечный пункт 2", InstructorId = 2
        },
        new()
        {
            Id = 3, Length = Random.Shared.Next(2, 21), Difficulty = "A+",
            StartPoint = "Начальный пункт 3", MiddlePoint = "Промежуточный пункт 3",
            FinishPoint = "Конечный пункт 3", InstructorId = 1
        },
        new()
        {
            Id = 4, Length = Random.Shared.Next(2, 21), Difficulty = "C+",
            StartPoint = "Начальный пункт 4", MiddlePoint = "Промежуточный пункт 4",
            FinishPoint = "Конечный пункт 4", InstructorId = 3
        },
        new()
        {
            Id = 5, Length = Random.Shared.Next(2, 21), Difficulty = "B+",
            StartPoint = "Начальный пункт 5", MiddlePoint = "Промежуточный пункт 5",
            FinishPoint = "Конечный пункт 5", InstructorId = 2
        },
        new()
        {
            Id = 6, Length = Random.Shared.Next(2, 21), Difficulty = "A+",
            StartPoint = "Начальный пункт 6", MiddlePoint = "Промежуточный пункт 6",
            FinishPoint = "Конечный пункт 6", InstructorId = 4
        },
        new()
        {
            Id = 7, Length = Random.Shared.Next(2, 21), Difficulty = "B-",
            StartPoint = "Начальный пункт 7", MiddlePoint = "Промежуточный пункт 7",
            FinishPoint = "Конечный пункт 7", InstructorId = 5
        },
        new()
        {
            Id = 8, Length = Random.Shared.Next(2, 21), Difficulty = "C-",
            StartPoint = "Начальный пункт 8", MiddlePoint = "Промежуточный пункт 8",
            FinishPoint = "Конечный пункт 8", InstructorId = 3
        },
        new()
        {
            Id = 9, Length = Random.Shared.Next(2, 21), Difficulty = "A",
            StartPoint = "Начальный пункт 9", MiddlePoint = "Промежуточный пункт 9",
            FinishPoint = "Конечный пункт 9", InstructorId = 1
        },
        new()
        {
            Id = 10, Length = Random.Shared.Next(2, 21), Difficulty = "A-",
            StartPoint = "Начальный пункт 10", MiddlePoint = "Промежуточный пункт 10",
            FinishPoint = "Конечный пункт 10", InstructorId = 1
        },
        new()
        {
            Id = 11, Length = Random.Shared.Next(2, 21), Difficulty = "B-",
            StartPoint = "Начальный пункт 11", MiddlePoint = "Промежуточный пункт 11",
            FinishPoint = "Конечный пункт 11", InstructorId = 2
        },
        new()
        {
            Id = 12, Length = Random.Shared.Next(2, 21), Difficulty = "C",
            StartPoint = "Начальный пункт 12", MiddlePoint = "Промежуточный пункт 12",
            FinishPoint = "Конечный пункт 12", InstructorId = 3
        },
    };
    
    // Список категорий инструкторов
    public static readonly List<string> Categories =
        new () { "A", "B", "C" };
    
    // Список категорий сложности туристических маршрутов
    public static readonly List<string> Difficulties = 
        new () { "A-", "A", "A+", "B-", "B", "B+", "C-", "C", "C+" };
}